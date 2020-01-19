using System.ComponentModel;
using System.IO;
using Life.Utilities;
using Trigger = Life.Utilities.Trigger;

namespace Files.Triggers
{
    public class Watcher : Trigger
    {
        private readonly FileSystemWatcher _watcher;

        private Watcher(Life.Trigger trigger)
            : base(trigger)
        {
        }

        [Category("Specific File")]
        [Editor(typeof (FileInfoEditor), typeof (FileInfoEditor))]
        public string Filename
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        [Category("Set of Files")]
        [Editor(typeof(DirectoryInfoEditor), typeof(DirectoryInfoEditor))]
        public string Path
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        [Category("Set of Files")]
        public string Filter
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public NotifyFilters Notify
        {
            get { return GetValue<NotifyFilters>(); }
            set { SetValue(value); }
        }


        public Watcher(Life.Trigger trigger, string filename, NotifyFilters notify = NotifyFilters.LastWrite)
            : this(trigger)
        {
            _watcher = new FileSystemWatcher
                {
                    Path = System.IO.Path.GetDirectoryName(filename),
                    NotifyFilter = notify,
                    EnableRaisingEvents = true,
                    Filter = System.IO.Path.GetFileName(filename)
                };
            _watcher.Changed += (sender, args) => OnTriggered(filename);
        }

        public Watcher(Life.Trigger trigger, string path, string filter, NotifyFilters notify = NotifyFilters.LastWrite)
            : this(trigger)
        {
            _watcher = new FileSystemWatcher
                {
                    Path = path,
                    NotifyFilter = notify,
                    EnableRaisingEvents = true,
                    Filter = filter
                };
            _watcher.Changed += (sender, args) => OnTriggered(System.IO.Path.Combine(path, filter));
        }
    }
}
