using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Facebook
{
    public static partial class Update
    {
        public const string PHOTO_FIELDS = "from.fields(id,name,updated_time),place.location,place.name,place.description,album,created_time,updated_time,tags,height,width,name,comments";

        public static FacebookPhoto Photo(long id, JToken json)
        {
            FacebookPhoto fb;
            if ((fb = Data.FacebookPhotos.FirstOrDefault(x => x.Id == id)) == null)
            {
                fb = new FacebookPhoto
                {
                    Id = id,
                    TimeCreated = json.TryGetValue<DateTime>("created_time")
                };
                if (fb.TimeCreated == default(DateTime))
                    return null;
                Data.FacebookPhotos.InsertOnSubmit(fb);
            }
            if (!json.Any())
                return fb;

            fb.Name = json.TryGetValue<string>("name");
            fb.Height = json.TryGetValue<int>("height");
            fb.Width = json.TryGetValue<int>("width");

            try
            {
                Data.SubmitChanges();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("There was an error saving the photo: {0} {1}", id, fb.Name), ex);
                return null;
            }

            var albumId = json["album"].TryGetValue<long?>("id");
            if (albumId != null)
            {
                var album = Album(albumId.Value, json["album"]);
                if (album != null)
                    fb.Album = album.Id;
                try
                {
                    Data.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("There was an error saving the photo: {0} {1}", id, fb.Name), ex);
                }
            }

            var locationId = json["place"].TryGetValue<long?>("id");
            if (locationId != null)
            {
                var location = Place(locationId.Value, json["place"]);
                if (location != null)
                    fb.Location = location.Id;
                try
                {
                    Data.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("There was an error saving the photo: {0} {1}", id, fb.Name), ex);
                }
            }

            var fromId = json["from"].TryGetValue<long?>("id");
            if (fromId != null)
            {
                var contact = Contact(fromId.Value, json["from"]);
                if (contact != null)
                    fb.Contact = contact.Id;
                try
                {
                    Data.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("There was an error saving the photo: {0} {1}", id, fb.Name), ex);
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
