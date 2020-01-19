using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Facebook
{
    public static partial class Update
    {
        public const string MESSAGE_FIELDS = "id,from.fields(id,name,updated_time),to.fields(id,name),message,updated_time,comments.fields(id,from.fields(id,name,updated_time),message,created_time)";
        public const string FEED_FIELDS = "id,from.fields(id,name,updated_time),to.fields(id,name),message,type,message,name,caption,description,link,updated_time,comments.fields(id,from.fields(id,name,updated_time),message,created_time)";
        
        public static FacebookMessage Message(long id, JToken json)
        {
            FacebookMessage fm;
            var keyTime = json.TryGetValue<DateTime?>("created_time") ?? json.TryGetValue<DateTime>("updated_time");
            if (keyTime == default(DateTime))
                return null;
            if ((fm = Data.FacebookMessages.FirstOrDefault(x => x.Id == id && x.TimeCreated == keyTime)) == null)
            {
                fm = new FacebookMessage
                    {
                        Id = id,
                        TimeCreated = keyTime
                    };
                Data.FacebookMessages.InsertOnSubmit(fm);
            }
            if (!json.Any())
                return fm;

            fm.Message = json.TryGetValue<string>("message");
            fm.TimeUpdated = json.TryGetValue<DateTime>("updated_time");
            if (fm.TimeUpdated == default(DateTime))
                fm.TimeUpdated = null;
            fm.Description = json.TryGetValue<string>("description");
            fm.Type = json.TryGetValue<string>("type");
            fm.Link = json.TryGetValue<string>("link");
            fm.Caption = json.TryGetValue<string>("caption");
            fm.Name = json.TryGetValue<string>("name");

            try
            {
                Data.SubmitChanges();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("There was an error saving the message: {0} {1}", id, fm.Message), ex);
                return null;
            }

            var fromId = json["from"].TryGetValue<long?>("id");
            if (fromId != null)
            {
                var contact = Contact(fromId.Value, json["from"]);
                if (contact != null)
                    fm.From = contact.Id;
                try
                {
                    Data.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("There was an error saving the message: {0} {1}", id, fm.Message), ex);
                }
            }

            var comments = json.TryGetValue<JObject>("comments");
            if (comments != null)
            {
                var client = new WebClient();
                do
                {
                    foreach (var comment in comments["data"])
                    {
                        var message = Message(id, comment);
                        try
                        {
                            Data.SubmitChanges();
                        }
                        catch (Exception ex)
                        {
                            Log.Error(
                                string.Format("There was an error saving the message: {0} {1}", id, message.Message),
                                ex);
                        }
                    }

                    try
                    {
                        string paging;
                        if ((paging = comments["paging"].TryGetValue<string>("next")) == null)
                            break;
                        var msgsData = client.DownloadData(paging);
                        var msg = Encoding.UTF8.GetString(msgsData);
                        comments = JObject.Parse(msg);
                    }
                    catch (WebException)
                    {
                        Thread.Sleep(60000);
                    }

                } while (comments["data"].Any());
            }

            return fm;
        }
    }
}
