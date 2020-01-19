using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Files.Activities;
using Life.Utilities;
using Life.Utilities.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace Files.Triggers
{
    public class Audited : Trigger
    {
        private Regex[] _matches;
        private bool _any;

        [Category("Conditions")]
        [Editor(typeof(CollectionEditor<EditableRegex[]>), typeof(CollectionEditor<EditableRegex[]>))]
        public Regex[] Matches
        {
            get { return GetValue<Regex[]>(); }
            set { SetValue(value); }
        }

        [Category("Conditions")]
        [Description("When set to true (default), any match fires the trigger, otherwise requires all matches to fire.")]
        public bool Any
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public Audited(Life.Trigger trigger, Regex[] matches, bool any = true) 
            : base(trigger)
        {
            _matches = matches;
            _any = any;
            //AuditFiles.AnyResulted + 
        }

        private void Resulted(Activity activity, Filesystem file)
        {
            OnTriggered(string.Format("The file {0} has been modified.", file.Filepath));
        }
    }
}
