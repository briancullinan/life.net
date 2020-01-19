using System.Windows;

namespace Life.Controls
{
    public class Vertex : DependencyObject
    {
        public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register(
            "Object", 
            typeof(UIElement), 
            typeof(Vertex), 
            new PropertyMetadata(null, OnChanged));

        public event RoutedEventHandler Changed;

        private static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var vertex = sender as Vertex;
            if (vertex != null && vertex.Changed != null)
                vertex.Changed(sender, new RoutedEventArgs());
        }

        public UIElement Object 
        {
            get { return (UIElement)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }
    }
}