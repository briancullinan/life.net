using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Files.Triggers;
using Life;
using Life.Annotations;
using Life.Utilities;

namespace Files.Controls
{
    /// <summary>
    /// Interaction logic for Filesystem.xaml
    /// </summary>
    public partial class Watcher : INotifyPropertyChanged
    {
        private Triggers.Watcher _trigger;

        public static readonly DependencyProperty FilepathProperty = DependencyProperty.Register(
            "Filename",
            typeof (string),
            typeof (Watcher),
            new PropertyMetadata(default(string)));

        public string Filename
        {
            get { return (string)GetValue(FilepathProperty); }
            set { SetValue(FilepathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
            "Path",
            typeof(string),
            typeof(Watcher),
            new PropertyMetadata(default(string)));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(
            "Filter",
            typeof(string),
            typeof(Watcher),
            new PropertyMetadata(default(string)));

        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }


        public Triggers.Watcher Trigger
        {
            get { return _trigger; }
            private set
            {
                _trigger = value;
                OnPropertyChanged();
            }
        }

        public Watcher(Triggers.Watcher filesystem)
        {
            InitializeComponent();
            Trigger = filesystem;
            Parameter param;
            if ((param = Trigger.Entity.Parameters.FirstOrDefault(x => x.Name == "Filename")) != null)
                Filename = param.Value;
            if ((param = Trigger.Entity.Parameters.FirstOrDefault(x => x.Name == "Path")) != null)
                Path = param.Value;
            if ((param = Trigger.Entity.Parameters.FirstOrDefault(x => x.Name == "Filter")) != null)
                Filter = param.Value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Open_File(object sender, RoutedEventArgs e)
        {

        }
    }
}
