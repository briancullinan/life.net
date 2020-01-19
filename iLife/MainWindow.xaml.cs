using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interactivity;
using iLife.Controls;
using iLife.Utilities;
using iLife.Utilities.Behaviors;

namespace iLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Plugins_Click(object sender, RoutedEventArgs e)
        {
            Tiles.Children.Clear();

            foreach (var plugin in App.Plugins)
            {
                var newPlugin = new Artifact
                {
                    Category = plugin.Name
                };
                newPlugin.MouseUp += newPlugin_MouseUp;
                newPlugin.Tag = plugin;
                Tiles.Children.Add(newPlugin);
                var behave = Interaction.GetBehaviors(newPlugin);
                behave.Add(new TiltBehavior
                {
                    KeepDragging = false
                });
            }
        }

        void newPlugin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);

            //((IPlugin)((Artifact)sender).Tag)
        }
    }
}
