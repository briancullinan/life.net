using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Life;
using Newtonsoft.Json.Linq;
using log4net;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Facebook.Activities
{
    public class SynchronizeMessages : Activity
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SynchronizePhotos));
        private readonly string _profile;
        private readonly string _credential;

        public SynchronizeMessages(Life.Activity activity, string profile = "", string credential = "Facebook.Profile")
            : base(activity)
        {
            _profile = profile;
            _credential = credential;
        }

        public override void Execute(object context, ActivityQueue queue, Trigger trigger)
        {
            var access = Utilities.GetAuthToken(_credential, _profile);

            Execute("inbox", access);
            Execute("outbox", access);
            Execute("feed", access);
        }

        private void Execute(string page, string access)
        {
            var client = new WebClient();
            JObject jMsg = null;

            // list messages
            do
            {
                var msgUrl = jMsg == null
                                    ? string.Format(
                                        "https://graph.facebook.com/me/" + page + "?" +
                                        "fields=" +
                                        (page == "feed" ? Update.FEED_FIELDS : Update.MESSAGE_FIELDS) +
                                        "&method=GET&format=json&access_token={0}",
                                        access)
                                    : HttpUtility.UrlDecode(jMsg["paging"].TryGetValue<string>("next"));

                try
                {
                    var msgsData = client.DownloadData(msgUrl);
                    var msg = Encoding.UTF8.GetString(msgsData);
                    jMsg = JObject.Parse(msg);
                }
                catch (WebException)
                {
                    Thread.Sleep(60000);
                    continue;
                }

                var msgJson = jMsg["data"];

                foreach (var json in msgJson)
                {
                    if (page == "feed")
                    {
                        var id = json.TryGetValue<string>("id").Split('_');
                        Update.Message(long.Parse(id[1]), json);
                        if (json.TryGetValue<string>("type") == "photo")
                            Update.Photo(long.Parse(id[1]), json);
                    }
                    else
                        Update.Message(json.TryGetValue<long>("id"), json);
                }

            } while (jMsg  == null || jMsg["data"].Any());
        }
    }
}
