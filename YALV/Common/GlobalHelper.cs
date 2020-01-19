using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using YALV.Domain;
using YALV.Properties;
using YALV.Providers;

namespace YALV.Common
{
    public class GlobalHelper
    {
        private const string DISPLAY_DATETIME_FORMAT = "MMM d, HH:mm:ss.fff";

        public const string LAYOUT_LOG4J = "http://jakarta.apache.org/log4j";

        public const int DEFAULT_REFRESH_INTERVAL = 30;

        public static string FOLDERS_FILE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YALVFolders.xml");

        public static string DisplayDateTimeFormat
        {
            get
            {
                string localizedFormat = Properties.Resources.GlobalHelper_DISPLAY_DATETIME_FORMAT;
                return String.IsNullOrWhiteSpace(localizedFormat) ? DISPLAY_DATETIME_FORMAT : localizedFormat;
            }
        }

        public static string GetTimeDelta(DateTime prevDate, DateTime currentDate)
        {
            double delta = (currentDate - prevDate).TotalSeconds;
            //if (DateTime.Compare(currentDate.Date, prevDate.Date) == 0)
            return String.Format(delta >= 0 ? Resources.GlobalHelper_getTimeDelta_Positive_Text : Resources.GlobalHelper_getTimeDelta_Negative_Text, delta.ToString(System.Globalization.CultureInfo.GetCultureInfo(Properties.Resources.CultureName)));
            //else
            //    return "-";
        }

        public static void DoEvents()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
                                                                 new System.Threading.ThreadStart(() => { }));
        }

        public static bool SaveFolderFile(IList<PathItem> folders, string path)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                if (folders != null)
                {
                    fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    streamWriter = new StreamWriter(fileStream);
                    foreach (PathItem item in folders)
                    {
                        string line = String.Format("<folder name=\"{0}\" path=\"{1}\" />", item.Name, item.Path);
                        streamWriter.WriteLine(line);
                    }
                    streamWriter.Close();
                    streamWriter = null;
                    fileStream.Close();
                    fileStream = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                string message = String.Format(Resources.GlobalHelper_SaveFolderFile_Error_Text, path, ex.Message);
                MessageBox.Show(message, Resources.GlobalHelper_SaveFolderFile_Error_Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            finally
            {
                if (streamWriter != null)
                    streamWriter.Close();
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        public static IList<PathItem> ParseFolderFile(string path)
        {
            FileStream fileStream = null;
            StreamReader streamReader = null;
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                    return null;

                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                streamReader = new StreamReader(fileStream, true);
                string sBuffer = string.Format("<root>{0}</root>", streamReader.ReadToEnd());
                streamReader.Close();
                streamReader = null;
                fileStream.Close();
                fileStream = null;

                var stringReader = new StringReader(sBuffer);
                var xmlTextReader = new XmlTextReader(stringReader) { Namespaces = false };

                IList<PathItem> result = new List<PathItem>();
                while (xmlTextReader.Read())
                {
                    if ((xmlTextReader.NodeType != XmlNodeType.Element) || (xmlTextReader.Name != "folder"))
                        continue;

                    PathItem item = new PathItem(xmlTextReader.GetAttribute("name"), xmlTextReader.GetAttribute("path"));
                    result.Add(item);
                }
                return result;
            }
            catch (Exception ex)
            {
                string message = String.Format(Resources.GlobalHelper_ParseFolderFile_Error_Text, path, ex.Message);
                MessageBox.Show(message, Resources.GlobalHelper_ParseFolderFile_Error_Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return null;
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        public static IList<LogItem> ParseLogFile(string path)
        {
            IEnumerable<LogItem> result = null;
            try
            {
                AbstractEntriesProvider provider = EntriesProviderFactory.GetProvider();
                result = provider.GetEntries(path);
                return result.ToList();
            }
            catch (Exception ex)
            {
                string message = String.Format(Resources.GlobalHelper_ParseLogFile_Error_Text, path, ex.Message);
                MessageBox.Show(message, Resources.GlobalHelper_ParseLogFile_Error_Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return result == null ? new List<LogItem>() : result.ToList();
            }
        }
    }
}