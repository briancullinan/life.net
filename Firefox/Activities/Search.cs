using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Life;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Firefox.Activities
{
    public class Search : Activity
    {
        public Search(Life.Activity activity) : base(activity)
        {
        }

        public override void Execute(object context, ActivityQueue queue, Trigger trigger)
        {
            
        }
    }
}
