using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Firefox
{
    public class moz_keywords
    {
        [PrimaryKey]
        public int id { get; set; }

        public string keyword { get; set; }
    }
}
