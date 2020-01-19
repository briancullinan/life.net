using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;
using System.Windows.Navigation;
using System.Windows.Controls;
using log4net;

namespace iLife
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(App));
        private static List<Utilities.IPlugin> _plugins;
        public static List<Utilities.IPlugin> Plugins
        {
            get
            {
                if (_plugins == null)
                    Load_Plugins();
                return _plugins;

            }
        }

        private static void Load_Plugins()
        {
            // loop through each group box an load the plugins
            if (_plugins == null)
            {
                _plugins = new List<Utilities.IPlugin>();
                var assemblyLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
                var pluginsDir = Directory.GetFiles(Path.Combine(assemblyLoc, "Plugins"));
                foreach (var dll in pluginsDir)
                {
                    if (Path.GetExtension(dll) != ".dll")
                        continue;

                    try
                    {
                        var plugin = Assembly.LoadFrom(dll);
                        var pluginTypes = plugin.GetTypes().Where(x => typeof(Utilities.IPlugin).IsAssignableFrom(x)).ToList();
                        foreach (var instance in pluginTypes.Select(type => (Utilities.IPlugin)Activator.CreateInstance(type)))
                        {
                            _plugins.Add(instance);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(string.Format("There was an error loading the plugin: {0}", dll), ex);
                    }
                }
            }

        }
    }
}
