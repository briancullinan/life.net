using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using Email.Activities;
using Life.Utilities;
using Life.Utilities.Converters;

namespace Email.Triggers
{
    [SecurityCritical]
    public class EmailReceived : Trigger
    {
        private readonly Regex[] _matches;
        private readonly bool _any;

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

        public EmailReceived(Life.Trigger trigger, Regex[] matches, bool any = true)
            : base(trigger)
        {
            /*
            var serlializer = new SoapFormatter();
            using (var mem = new MemoryStream())
            {
                var regex = new Regex(@"From\s*:\s*6144256054\@vtext\.com", RegexOptions.IgnoreCase);
                serlializer.Serialize(mem, new[] { regex });
                Console.WriteLine(System.Text.Encoding.ASCII.GetString(mem.ToArray()));
            }
            */
            _matches = matches;
            _any = any;
        }

        private void CheckMailOnReceived(List<Message> messages)
        {
            // find any message that match the specified headers
            var matches =
                messages.Where(
                    msg => _any
                               // any of the matchings headers can trigger
                               ? msg.Headers.Any(
                                   hdr => _matches.Any(regex => regex.IsMatch(hdr.Key + ":" + hdr.Value)))
                               // all of the expressions must have a matching header
                               : _matches.All(
                                   regex => msg.Headers.Any(hdr => regex.IsMatch(hdr.Key + ":" + hdr.Value))))
                        .ToList();
            foreach (var message in matches)
            {
                OnTriggered(message);
            }
        }
    }
}
