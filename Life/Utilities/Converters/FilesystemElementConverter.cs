using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Life.Triggers;

namespace Life.Utilities.Converters
{
    public class FilesystemElementConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(Filesystem) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var s = value as Filesystem;
            return s != null ? new Controls.Filesystem(s) : base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(Filesystem) && value as Controls.Filesystem != null)
                return ((Controls.Filesystem)value).Trigger;
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
