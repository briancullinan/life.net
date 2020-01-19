using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Facebook
{
    public static partial class Update
    {
        private static readonly DatalayerDataContext Data = new DatalayerDataContext();
        private static readonly ILog Log = LogManager.GetLogger(typeof(Update));
    }
}
