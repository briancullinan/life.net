using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using AvalonDock.Themes;
using Life.Annotations;
using log4net.Core;

namespace Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private Theme _avalonDockTheme = new MetroTheme();

        public static readonly DependencyProperty LevelsProperty = DependencyProperty.Register(
            "Levels",
            typeof (IEnumerable<Level>),
            typeof (MainWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SearchProperty = DependencyProperty.Register(
            "Search", 
            typeof (string), 
            typeof (MainWindow),
            new PropertyMetadata(null));

        public static readonly DependencyProperty PeopleProperty = DependencyProperty.Register(
            "People",
            typeof (List<string>),
            typeof (MainWindow),
            new PropertyMetadata(null));

        public Theme AvalonDockTheme
        {
            get
            {
                return _avalonDockTheme;
            }

            set
            {
                if (Equals(_avalonDockTheme, value)) return;
                _avalonDockTheme = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Level> Levels
        {
            get { return (IEnumerable<Level>) GetValue(LevelsProperty); }
            set { SetValue(LevelsProperty, value); }
        }

        public string Search
        {
            get { return (string) GetValue(SearchProperty); }
            set { SetValue(SearchProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            Levels = typeof (Level).GetProperties(BindingFlags.Static | BindingFlags.Public)
                                   .Where(x => x.PropertyType == typeof (Level))
                                   .Select(x => (Level) x.GetValue(null))
                                   .OrderBy(x => x.Value)
                                   .ToList();
        }
    }
}
