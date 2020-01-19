using System;
using System.Linq;
using System.Net;
using System.Text;
using Life;
using Newtonsoft.Json.Linq;
using log4net;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Facebook.Activities
{
    public class SynchronizeContacts : Activity
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SynchronizeContacts));
        private readonly string _profile;
        private readonly string _credential;

        public SynchronizeContacts(Life.Activity activity, string profile = "", string credential = "Facebook.Profile")
            : base(activity)
        {
            _profile = profile;
            _credential = credential;
        }

        public override void Execute(object context, ActivityQueue queue, Trigger trigger)
        {
            var access = Utilities.GetAuthToken(_credential, _profile);

            // list contacts
            var contactsUrl =
                string.Format(
                    "https://graph.facebook.com/me?fields=id%2Cname%2Cfriends&method=GET&format=json&access_token={0}",
                    access);

            var client = new WebClient();
            var contactsData = client.DownloadData(contactsUrl);
            var contacts = Encoding.UTF8.GetString(contactsData);
            var contactsJson = JObject.Parse(contacts);
            
            // save contacts to the database
            foreach (var json in contactsJson["friends"]["data"])
            {
                var id = json.Value<long>("id");
                Update.Contact(id, json);
            }
        }
    }
}
