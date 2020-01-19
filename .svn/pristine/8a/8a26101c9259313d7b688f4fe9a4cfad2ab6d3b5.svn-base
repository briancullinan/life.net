using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Firefox
{
    public class moz_favicons
    {
        [PrimaryKey]
        public int id { get; set; }

        public string url { get; set; }

        public byte[] data { get; set; }

        public string mime_type { get; set; }

        public long expiration { get; set; }

        [Indexed(Unique = true)]
        public string guid { get; set; }
    }
}
