using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Text.RegularExpressions;
using log4net;

namespace fsAudit
{
    public partial class Service : ServiceBase, iLife.Utilities.IPlugin
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Service));
        static List<EventLog> myNewLog = new List<EventLog>();
        static int max = 0;
        protected delegate void Startup(string computer);
        protected delegate void Listener(IPAddress ip, int port);
        static List<int> ports;
        event EventHandler AddWatch;

        // 0b100000000110011111
        public enum AccessCodes : int
        {
            READ = 0x1,
            CREATE = 0x2,

            DELETE = 0x10000,
            READ_CONTROL = 0x20000,
            WRITE_DAC = 0x40000,
            WRITE_OWNER = 0x80000,
            SYNCHRONIZE = 0x100000
        }

        public Service()
        {
            InitializeComponent();
        }

        public string Name
        {
            get
            {
                return Properties.Resources.Name;
            }
        }

        static void Main(string[] args)
        {
            Service service = new Service();
            if (Environment.UserInteractive)
            {
                service.OnStart(args);
                Console.WriteLine("Press enter to stop program");
                Console.ReadLine();
                service.OnStop();
            }
            else
            {
                ServiceBase.Run(service);
            }

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ports = ConfigurationManager.AppSettings["Ports"]
                    .Split(',')
                    .Select(x => Int32.Parse(x))
                    .ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                ports = new List<int> { 60 };
            }

            // start up folder watches
            var watches = (System.Configuration.ClientSettingsSection)ConfigurationManager.GetSection("FSAudit.Watches");
            foreach (SettingElement watch in watches.Settings)
            {
                Startup worker = new Startup(InitializeEventLogListener);
                worker.BeginInvoke(watch.Name, null, null);
            }

            // start up connection listeners
            foreach (ConnectionStringSettings conn in ConfigurationManager.ConnectionStrings)
            {
                try
                {
                    var builder = new System.Data.Common.DbConnectionStringBuilder();
                    builder.ConnectionString = conn.ConnectionString.Replace("{", "\"").Replace("}", "\"");
                    Listener worker = new Listener(InitializeTcpStringListener);
                    IPHostEntry iphostinfo = Dns.GetHostEntry(builder["data source"].ToString());
                    IPAddress ipad = iphostinfo.AddressList[0];
                    worker.BeginInvoke(ipad, Int32.Parse(builder["Port"].ToString()), null, null);
                }
                catch
                {
                    Log.Error("Could not start connection " + conn.ConnectionString);
                }
            }
        }

        static void InitializeEventLogListener(string computer)
        {
            string comp = computer;
            try
            {
                EventLog new_log = new EventLog("Security", computer, "File System");
                new_log.EnableRaisingEvents = true;
                new_log.EntryWritten += (o, e) => ProcessEntry(e.Entry, comp, new FSAuditDataContext());

                // maintain referenced to global list
                myNewLog.Add(new_log);

                FSAuditDataContext Life = new FSAuditDataContext();
                // Enumerate through current log entries
                foreach (EventLogEntry entry in new_log.Entries)
                {
                    ProcessEntry(entry, comp, Life);
                }
            }
            catch (Exception e)
            {
                if (e is IndexOutOfRangeException)
                {
                    Log.Error("Events purged before loading finished.", e);
                }
                else if (e is InvalidOperationException)
                {
                    // must need a listener because it isn't windows
                    InitializeTcpStringListener(IPAddress.Any, ports[0]);
                }
            }
        }

        static void ProcessEntry(EventLogEntry entry, string computer, FSAuditDataContext fsAudit)
        {
            // Do something with log entries
            if ((entry.InstanceId == 4656 || entry.InstanceId == 4663) && entry.Index > max)
            {
                var events = fsAudit.Filesystems.Where(x => x.EventId == entry.Index);
                if (events.Count() == 0)
                {
                    Filesystem log = new Filesystem();
                    int num = 0;
                    if (entry.InstanceId == 4656)
                        num = Int32.Parse(entry.ReplacementStrings[10].Substring(2), System.Globalization.NumberStyles.HexNumber);
                    else if (entry.InstanceId == 4663)
                        num = Int32.Parse(entry.ReplacementStrings[9].Substring(2), System.Globalization.NumberStyles.HexNumber);

                    log.Type = "";

                    if ((num & (int)AccessCodes.DELETE) == (int)AccessCodes.DELETE)
                        log.Type += (log.Type != "" ? "," : "") + "Delete";

                    if ((num & (int)AccessCodes.READ_CONTROL) == (int)AccessCodes.READ_CONTROL || (num & (int)AccessCodes.READ) == (int)AccessCodes.READ)
                        log.Type += (log.Type != "" ? "," : "") + "Read";

                    if ((num & (int)AccessCodes.WRITE_DAC) == (int)AccessCodes.WRITE_DAC || (num & (int)AccessCodes.WRITE_OWNER) == (int)AccessCodes.WRITE_OWNER)
                        log.Type += (log.Type != "" ? "," : "") + "Write";

                    if ((num & (int)AccessCodes.CREATE) == (int)AccessCodes.CREATE)
                        log.Type += (log.Type != "" ? "," : "") + "Create";

                    log.Filepath = entry.ReplacementStrings[6];
                    log.Time = entry.TimeGenerated.ToUniversalTime();
                    log.EventId = entry.Index;
                    log.Username = entry.ReplacementStrings[2] + "\\" + entry.ReplacementStrings[1];
                    log.AccessMask = num;
                    log.Success = entry.EntryType == EventLogEntryType.SuccessAudit;
                    log.Computer = computer;
                    fsAudit.Filesystems.InsertOnSubmit(log);

                    try
                    {
                        fsAudit.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        Log.Error("Error saving audit entry.", e);
                    }
                }
            }
        }

        protected override void OnStop()
        {
        }

        static private void InitializeTcpStringListener(IPAddress ip, int port)
        {
            Dictionary<decimal, Dictionary<string, string>> audits = new Dictionary<decimal, Dictionary<string, string>>();

            try
            {
                // TcpListener server = new TcpListener(port);
                TcpListener server = new TcpListener(ip, port);

                // Start listening for client requests.
                Log.Info("Starting listener on " + ip + ":" + port);
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[1024];
                string data = "";

                // Enter the listening loop.
                while (true)
                {
                    Log.Info("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();

                    Log.Info("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data += System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        // Process the data sent by the client
                        string[] lines = data.Split('\n');
                        if (lines.Length > 1)
                        {
                            for (int j = 0; j < lines.Length - 1; j++)
                            {
                                // remove processesed data
                                data = data.Substring(lines[j].Length + 1);

                                // read data in to a dictionary
                                var new_data = Regex
                                    .Matches(lines[j], @"((?<key>[a-z0-9]*)=(""((?<value>.*?)(?<!\\)"")|(?<value>[^""\s]+))\s*)", RegexOptions.IgnoreCase)
                                    .Cast<Match>()
                                    .Select(m => new KeyValuePair<string, string>(m.Groups["key"].Value, m.Groups["value"].Value))
                                    .ToDictionary(x => x.Key, x => x.Value);

                                if (new_data.ContainsKey("msg"))
                                {
                                    // get key for this data
                                    decimal key = decimal.Parse(new_data["msg"].Replace("audit(", "").Replace(")", "").Replace(":", ""));

                                    // interpret path
                                    if (new_data.ContainsKey("type") && new_data["type"] == "PATH" && new_data.ContainsKey("name"))
                                    {
                                        try
                                        {
                                            IEnumerable<string> chars = Enumerable.Range(0, new_data["name"].Length / 2)
                                                .Select(x => new_data["name"].Substring(x * 2, 2));
                                            if (chars.Contains("2F"))
                                            {

                                                new_data["name"] = String.Concat(
                                                    chars
                                                    .Select(cs => (char)Int32.Parse(cs, NumberStyles.HexNumber)));
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        new_data["mode"] = Enumerable.Range(0, new_data["mode"].Length)
                                            .Sum(x => Int32.Parse(new_data["mode"].Substring(new_data["mode"].Length - x - 1, 1)) << (x * 3))
                                            .ToString();
                                    }


                                    // merge to get entire record
                                    if (new_data.ContainsKey("type") &&
                                        (new_data["type"] == "CWD" || new_data["type"] == "PATH" || new_data["type"] == "SYSCALL"))
                                    {
                                        if (!audits.ContainsKey(key))
                                            audits[key] = new Dictionary<string, string>();

                                        audits[key] = audits[key]
                                            .Union(new_data.Where(x => !audits[key].ContainsKey(x.Key)))
                                            .ToDictionary(x => x.Key, x => x.Value);
                                    }

                                    // add the record to the datacontext, PATH is the last bit of information
                                    if (new_data.ContainsKey("type") && new_data["type"] == "PATH" &&
                                        // make sure all the data is transferred
                                        audits[key].ContainsKey("syscall") &&
                                        audits[key].ContainsKey("name") &&
                                        audits[key].ContainsKey("cwd"))
                                    {

                                        if (audits[key]["name"].Contains("2003-12-24 Beach"))
                                            Debug.WriteLine(String.Concat(audits[key].Select(x => x.Key + "=" + x.Value + "\n")) + "\n");

                                        Filesystem log = new Filesystem();
                                        if (audits[key]["syscall"] == "open")
                                            log.Type = "Read";
                                        else
                                        {
                                            log.Type = audits[key]["syscall"];
                                        }
                                        log.Filepath = audits[key]["cwd"] + "/" + audits[key]["name"];
                                        log.Time = new DateTime(621355968000000000 + Convert.ToInt64(key * 10000000)).ToUniversalTime();
                                        log.EventId = decimal.Parse(new_data["msg"].Split(':').First());
                                        log.Username = audits[key]["uid"];
                                        log.AccessMask = int.Parse(new_data["mode"]);
                                        log.Success = true;
                                        log.Computer = audits[key]["node"];
                                        //log.Save();

                                        audits.Remove(key);
                                    }
                                }
                            }
                        }
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                Log.Error("SocketException: {0}", ex);
            }

        }
    }
}
