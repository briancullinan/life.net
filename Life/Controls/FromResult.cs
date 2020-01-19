using System;
using System.Runtime.Serialization;

namespace Life.Controls
{
    [Serializable]
    public class FromResult : ISerializable
    {
        public FromResult(SerializationInfo info, StreamingContext context)
        {
            From = (string)info.GetValue("from", typeof (string));
            Count = (int)info.GetValue("count", typeof (int));
        }

        public FromResult()
        {
            
        }

        public string From { get; set; }

        public int Count { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("from", From);
            info.AddValue("count", Count);
        }
    }
}