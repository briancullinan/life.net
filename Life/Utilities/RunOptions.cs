using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Utilities
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RunOptionsAttribute : Attribute
    {
        private readonly RunOptions _ops;

        public RunOptions Options
        {
            get { return _ops; }
        }

        public RunOptionsAttribute()
            : this (RunOptions.Default)
        {
            
        }

        public RunOptionsAttribute(RunOptions options)
        {
            _ops = options;
        }
    }

    public enum RunOptions
    {
        SingleThread,
        MultipleThreads,
        Processor1,
        Processor2,
        Processor3,
        Processor4,
        Processor5,
        Memory1,
        Memory2,
        Memory3,
        Memory4,
        Memory5,
        HardDisk1,
        HardDisk2,
        HardDisk3,
        HardDisk4,
        HardDisk5,
        Network1,
        Network2,
        Network3,
        Network4,
        Network5,
        Default = SingleThread | Processor1 | Memory1 | HardDisk1 | Network1
    }
}
