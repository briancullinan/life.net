using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Life;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Files.Activities
{
    public class Organize : Activity
    {
        public Organize(Life.Activity activity) : base(activity)
        {
        }

        public override void Execute(object context, ActivityQueue queue, Trigger trigger)
        {
            
        }
    }
}
