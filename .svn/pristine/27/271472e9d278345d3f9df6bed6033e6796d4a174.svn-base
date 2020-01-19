using System.Windows;
using Microsoft.Win32;

namespace Life.Controls
{
    /// <summary>
    /// Interaction logic for FileInfo.xaml
    /// </summary>
    public partial class DirectoryInfo
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(string),
            typeof(DirectoryInfo),
            new PropertyMetadata(string.Empty));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            private set
            {
                SetValue(ValueProperty, value);
            }
        }

        public DirectoryInfo()
        {
            InitializeComponent();
        }

        private void Open_File(object sender, RoutedEventArgs e)
        {
            var open = new OpenFileDialog
                {
                    Filter = "All Folders|.",
                    Multiselect = false
                };
            if (open.ShowDialog(Application.Current.MainWindow) == true)
            {
                Value = open.FileName;
            }
        }
    }
}
