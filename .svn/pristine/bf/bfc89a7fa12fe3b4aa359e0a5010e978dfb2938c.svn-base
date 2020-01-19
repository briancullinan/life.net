using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Life.Utilities.Converters
{
    public class EditableRegexConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context,
        Type sourceType)
        {
            return sourceType == typeof(EditableRegex) || sourceType == typeof(Regex);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value)
        {
            return value.GetType() == typeof (Regex)
                       ? (object)new EditableRegex
                       {
                           MatchTimeout = ((Regex)value).MatchTimeout,
                           Options = ((Regex)value).Options,
                           Pattern = ((Regex)value).ToString()
                       }
                       : new Regex(((EditableRegex)value).Pattern, ((EditableRegex)value).Options,
                                   ((EditableRegex)value).MatchTimeout);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context,
            Type destinationType)
        {
            return destinationType == typeof(EditableRegex) || destinationType == typeof(Regex);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return destinationType == typeof (EditableRegex)
                       ? (object) new EditableRegex
                           {
                               MatchTimeout = ((Regex) value).MatchTimeout,
                               Options = ((Regex) value).Options,
                               Pattern = ((Regex) value).ToString()
                           }
                       : new Regex(((EditableRegex) value).Pattern, ((EditableRegex) value).Options,
                                   ((EditableRegex) value).MatchTimeout);
        }
    }

    [TypeConverter(typeof(EditableRegexConverter))]
    public class EditableRegex
    {
        public string Pattern { get; set; }

        public TimeSpan MatchTimeout { get; set; }

        public RegexOptions Options { get; set; }

        public EditableRegex()
        {
            MatchTimeout = Regex.InfiniteMatchTimeout;
        }
    }
}
