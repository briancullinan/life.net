using System;
using System.Runtime.Serialization;

namespace Life.Controls
{
    [Serializable]
    public class MessageRequest : ISerializable
    {
        public string From { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public MessageRequest()
        {
            
        }

        public MessageRequest(SerializationInfo info, StreamingContext context)
        {
            From = (string)info.GetValue("from", typeof(string));
            Message = (string)info.GetValue("message", typeof(string));
            Time = (DateTime)info.GetValue("time", typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("from", From);
            info.AddValue("message", Message);
            info.AddValue("time", Time);
        }
    }
}