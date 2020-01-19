using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Life;
using Life.Controls;
using Life.Utilities.Extensions;

namespace Email
{
    public class DateToParentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime?)values.FirstOrDefault(x => x is DateTime);
            var el = (FrameworkElement)values.FirstOrDefault(x => x is FrameworkElement);
            if (el == null || date == null)
                return null;
            var navigator = el.FindParent<Navigator>();
            var label = navigator.Children.OfType<Label>()
                     .FirstOrDefault(x => x.Content.ToString() == date.Value.Year.ToString(CultureInfo.InvariantCulture));
            if (label == null)
            {
                var timeline = navigator.Children.OfType<MenuItem>()
                                        .First(x => x.Name == "Timeline");
                label = new Label
                    {
                        Content = date.Value.Year
                    };
                navigator.Children.Add(label);
                label.SetValue(Navigator.ParentProperty, timeline);
            }
            return label;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}