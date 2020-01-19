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
        public static FacebookPlace Place(long id, JToken json)
        {
            FacebookPlace fb;
            if ((fb = Data.FacebookPlaces.FirstOrDefault(x => x.Id == id)) == null)
            {
                fb = new FacebookPlace
                {
                    Id = id,
                    Name = json.TryGetValue<string>("name")
                };
                if (fb.Name == default(string))
                    return null;
                Data.FacebookPlaces.InsertOnSubmit(fb);
            }
            if (!json.Any())
                return fb;

            fb.Name = json.TryGetValue<string>("name");
            fb.Latitude = json.TryGetValue<double>("latitude");
            fb.Longitude = json.TryGetValue<double>("longitude");
            fb.City = json.TryGetValue<string>("city");
            fb.Country = json.TryGetValue<string>("country");
            fb.State = json.TryGetValue<string>("state");
            fb.Street = json.TryGetValue<string>("street");

            try
            {
                Data.SubmitChanges();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("There was an error saving the place: {0} {1}", id, fb.Name), ex);
                return null;
            }

            return fb;
        }
    }
}
