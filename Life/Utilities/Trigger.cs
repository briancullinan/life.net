using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Life.Utilities.Extensions;
using log4net;

namespace Life.Utilities
{
    public delegate void TriggerHandler(Trigger trigger, object state);

    public abstract class Trigger : Parameters //, ISerializable
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Trigger));
        private Life.Trigger _trigger;
        private string _inAddition;

        protected Trigger(Life.Trigger trigger)
        {
            _trigger = trigger;
        }

        /*public Trigger(SerializationInfo info, StreamingContext context)
        {
            var entity = (int)info.GetValue("entity", typeof(int));
            _trigger = App.AppContext.Triggers.FirstOrDefault(x => x.Id == entity);
            var args = Args(_trigger, )
            var constructor = GetType().GetConstructor();
            constructor.
        }
        */

        private TriggerHandler _triggered;
        public event TriggerHandler Triggered
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                TriggerHandler handler2;
                var log = _triggered;
                do
                {
                    handler2 = log;
                    var handler3 = (TriggerHandler)Delegate.Combine(handler2, value);
                    log = Interlocked.CompareExchange(ref _triggered, handler3, handler2);
                }
                while (log != handler2);

                OnPropertyChanged();
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                TriggerHandler handler2;
                var log = _triggered;
                do
                {
                    handler2 = log;
                    var handler3 = (TriggerHandler)Delegate.Remove(handler2, value);
                    log = Interlocked.CompareExchange(ref _triggered, handler3, handler2);
                }
                while (log != handler2);

                OnPropertyChanged();
            }
        }

        public Life.Trigger Entity
        {
            get { return _trigger; } internal set { _trigger = value; }
        }

        [Category("Common")]
        public bool Enabled
        {
            get { return Entity.Enabled; }
            set
            {
                Entity.Enabled = value;
                App.AppContext.SubmitChanges();
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get
            {
                var parameters = Args(Entity, GetType());
                var result = string.Join("\n", parameters.Select(x => string.Format("{0} [{1}]", x, (x ?? new object()).GetType())));
                result += _inAddition;
                return result;
            }
            protected set
            {
                _inAddition = value;
                OnPropertyChanged();
            }
        }

        protected void OnTriggered(object state)
        {
            if (_triggered != null)
                _triggered(this, state);
        }

        protected void SetValue(object obj, [CallerMemberName] string propertyName = null)
        {
            if (!this.IsConstructed())
                Log.Warn(string.Format("Value being set during construction: {0} [{1}]", propertyName, GetType()));

            Set(Entity, GetType(), propertyName, obj);
            OnPropertyChanged(propertyName);
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            var arg = Arg(Entity, GetType(), propertyName);
            if(arg is T)
                return (T)arg;
            return default(T);
        }

        /*public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("entity", Entity.Id);
        }
         */
    }
}