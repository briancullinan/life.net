﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SQLite;

namespace Firefox
{
    internal class moz_bookmarks
    {
        [PrimaryKey]
        public int id { get; set; }

        [Indexed]
        public int? type { get; set; }

        [DefaultValue(null)]
        [Indexed]
        public int? fk { get; set; }

        [Indexed]
        public int? parent { get; set; }

        [Indexed]
        public int? position { get; set; }

        public string title { get; set; }

        public int? keyword_id { get; set; }

        public string folder_type { get; set; }

        public long? dateAdded { get; set; }

        [Indexed]
        public long? lastModified { get; set; }

        [Indexed(Unique = true)]
        public string guid { get; set; }
    }
}