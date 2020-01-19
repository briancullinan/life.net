using System.ComponentModel;
using SQLite;

namespace Firefox
{
    public class moz_places
    {
        [PrimaryKey]
        public int id { get; set; }

        [Indexed(Unique = true)]
        public string url { get; set; }

        public string title { get; set; }

        [Indexed]
        public string rev_host { get; set; }

        [Indexed]
        [DefaultValue(0)]
        public int? visit_count { get; set; }

        [DefaultValue(false)]
        public bool hidden { get; set; }

        [DefaultValue(false)]
        public bool typed { get; set; }

        [Indexed]
        public int? favicon_id { get; set; }

        [Indexed]
        [DefaultValue(-1)]
        public int frecency { get; set; }

        [Indexed]
        public long? last_visit_date { get; set; }

        [Indexed(Unique = true)]
        public string guid { get; set; }
    }
}