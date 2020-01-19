using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Life;
using Newtonsoft.Json.Linq;
using log4net;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Facebook.Activities
{
    public class SynchronizeEvents : Activity
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SynchronizePhotos));
        private readonly string _profile;
        private readonly string _credential;

        public SynchronizeEvents(Life.Activity activity, string profile = "", string credential = "Facebook.Profile")
            : base(activity)
        {
            _profile = profile;
            _credential = credential;
        }

        public override void Execute(object context, ActivityQueue queue, Trigger trigger)
        {
            var access = Utilities.GetAuthToken(_credential, _profile);

            var client = new WebClient();
            // list contacts
            JObject jEvent = null;
            do
            {
                var eventsUrl = jEvent == null
                                    ? string.Format(
                                        "https://graph.facebook.com/me/events?" +
                                        "fields=" +
                                        Update.EVENT_FIELDS +
                                        "&method=GET&format=json&access_token={0}",
                                        access)
                                    : jEvent["paging"].TryGetValue<string>("next");

                var eventsData = client.DownloadData(eventsUrl);
                var events = Encoding.UTF8.GetString(eventsData);

                jEvent = JObject.Parse(events);
                var eventsJson = jEvent["data"];

                foreach (var json in eventsJson)
                {
                    var id = json.TryGetValue<long>("id");
                    Update.Event(id, json);
                }
            } while (jEvent["data"].Any());
        }
    }
}
