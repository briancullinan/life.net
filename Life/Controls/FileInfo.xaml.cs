using System.Windows;
using Microsoft.Win32;

namespace Life.Controls
{
    /// <summary>
    /// Interaction logic for FileInfo.xaml
    /// </summary>
    public partial class FileInfo
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof (string),
            typeof(FileInfo),
            new PropertyMetadata(string.Empty));

        public string Value
        {
            get { return (string) GetValue(ValueProperty); }
            private set
            {
                SetValue(ValueProperty, value);
            }
        }

        public FileInfo()
        {
            InitializeComponent();
        }

        private void Open_File(object sender, RoutedEventArgs e)
        {
            var open = new OpenFileDialog
                {
                    Filter = "All Files|*.*",
                    Multiselect = false
                };
            if (open.ShowDialog(Application.Current.MainWindow) == true)
            {
                Value = open.FileName;
            }
        }
    }
}
