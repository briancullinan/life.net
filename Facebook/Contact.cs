using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Facebook
{
    public static partial class Update
    {
        public const string CONTACT_FIELDS = "name,username,updated_time,birthday,gender,location";

        public static FacebookContact Contact(long id, JToken json)
        {
            FacebookContact fb;
            if ((fb = Data.FacebookContacts.FirstOrDefault(x => x.Id == id)) == null)
            {
                fb = new FacebookContact
                {
                    Id = id,
                    Name = json.TryGetValue<string>("name")
                };
                if (fb.Name == default(string))
                    return null;
                Data.FacebookContacts.InsertOnSubmit(fb);
            }
            if (!json.Any())
                return fb;

            fb.Name = json.TryGetValue<string>("name");
            string username;
            if ((username = json.TryGetValue<string>("username")) != null)
                fb.Username = username;
            fb.LastUpdated = json.TryGetValue<DateTime>("updated_time");
            DateTime birthday;
            if((birthday = json.TryGetValue<DateTime>("birthday")) != default(DateTime))
                fb.Birthday = birthday;
            string gender;
            if ((gender = json.TryGetValue<string>("gender")) != null)
                fb.Gender = gender.IndexOf("male", StringComparison.InvariantCultureIgnoreCase) > -1
                                ? "M"
                                : "F";

            try
            {
                Data.SubmitChanges();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("There was an error saving the contact: {0} {1}", id, fb.Name), ex);
                return null;
            }

            var locationId = json["location"].TryGetValue<long?>("id");
            if (locationId != null)
            {
                var location = Place(locationId.Value, json["location"]);
                if (location != null)
                    fb.Location = location.Id;
                try
                {
                    Data.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("There was an error saving the contact: {0} {1}", id, fb.Name), ex);
                }
            }

            return fb;
        }
    }
}
