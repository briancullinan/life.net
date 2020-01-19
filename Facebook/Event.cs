using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Facebook
{
    public partial class Update
    {
        public const string EVENT_FIELDS = "owner.fields(id,name),description,start_time,end_time,location,venue,name,updated_time,rsvp_status";

        public static FacebookEvent Event(long id, JToken json)
        {
            FacebookEvent fb;
            if ((fb = Data.FacebookEvents.FirstOrDefault(x => x.Id == id)) == null)
            {
                fb = new FacebookEvent
                {
                    Id = id,
                    Name = json.TryGetValue<string>("name")
                };
                if (fb.Name == default(string))
                    return null;
                Data.FacebookEvents.InsertOnSubmit(fb);
            }
            if (!json.Any())
                return fb;

            fb.Name = json.TryGetValue<string>("name");
            fb.Description = json.TryGetValue<string>("description");
            fb.TimeStart = json.TryGetValue<DateTime>("start_time");
            fb.TimeEnd = json.TryGetValue<DateTime>("end_time");
            if (fb.TimeEnd == default(DateTime))
                fb.TimeEnd = null;
            fb.TimeUpdated = json.TryGetValue<DateTime>("updated_time");
            if (fb.TimeUpdated == default(DateTime))
                fb.TimeUpdated = null;
            fb.Response = json.TryGetValue<string>("rsvp_status");

            try
            {
                Data.SubmitChanges();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("There was an error saving the event: {0} {1}", id, fb.Name), ex);
                return null;
            }

            var fromId = json["owner"].TryGetValue<long?>("id");
            if (fromId != null)
            {
                var contact = Contact(fromId.Value, json["owner"]);
                if (contact != null)
                    fb.Contact = contact.Id;
                try
                {
                    Data.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("There was an error saving the event: {0} {1}", id, fb.Name), ex);
                }
            }

            var locationId = json.TryGetValue<long?>("id");
            if (locationId != null)
            {
                var location = Place(locationId.Value, json["venue"]);
                if (location != null)
                    fb.Place = location.Id;
                try
                {
                    Data.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("There was an error saving the event: {0} {1}", id, fb.Name), ex);
                }
            }

            var comments = json.TryGetValue<JObject>("comments");
            if (comments != null)
            {
                Message(id, json);
            }

            return fb;
        }
    }
}
