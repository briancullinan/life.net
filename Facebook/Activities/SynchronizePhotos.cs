using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Life;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Facebook.Activities
{
    public class SynchronizePhotos : Activity
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SynchronizePhotos));
        private readonly string _profile;
        private readonly string _credential;

        public SynchronizePhotos(Life.Activity activity, string profile = "", string credential = "Facebook.Profile")
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
            JObject jPhoto = null;
            do
            {
                //me?fields=id,name,photos.fields(from,source,place,created_time,name,id,height,width,album)
                var photosUrl = jPhoto == null
                                    ? string.Format(
                                        "https://graph.facebook.com/me/photos?" +
                                        "fields=" + 
                                        Update.PHOTO_FIELDS + 
                                        "&method=GET&format=json&access_token={0}",
                                        access)
                                    : jPhoto["paging"].TryGetValue<string>("next");

                var photosData = client.DownloadData(photosUrl);
                var photos = Encoding.UTF8.GetString(photosData);

                jPhoto = JObject.Parse(photos);
                var photosJson = jPhoto["data"];

                foreach (var json in photosJson)
                {
                    var id = json.TryGetValue<long>("id");
                    Update.Photo(id, json);
                }
            } while (jPhoto["data"].Any());
        }
    }
}
