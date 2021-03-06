﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using Life.Utilities.Extensions;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using log4net;

namespace Life.Utilities
{
    /// <summary>
    /// Occurs when an activity has produces a result that may be interesting to other triggers and activities.
    /// </summary>
    /// <param name="result">The object representing the resulting state.</param>
    /// <param name="queue">The queue item that triggered the activity to begin in the first place.</param>
    /// <param name="activity">The activity that produced the result.</param>
    public delegate void ActivityResult(object result, ActivityQueue queue, Activity activity);

    public abstract class Activity : Parameters
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Activity));
        private Life.Activity _activity;
        private string _inAddition;
        private ActivityResult _resulted;
        private readonly List<Trigger> _resultHistory = new List<Trigger>();
        private readonly List<Trigger> _result = new List<Trigger>();

        public List<Trigger> ResultHistory
        {
            get { return _resultHistory; }
        }

        public event ActivityResult Resulted
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                ActivityResult handler2;
                var log = _resulted;
                do
                {
                    handler2 = log;
                    var handler3 = (ActivityResult)Delegate.Combine(handler2, value);
                    log = Interlocked.CompareExchange(ref _resulted, handler3, handler2);
                }
                while (log != handler2);

                // store the trigger the event calls in a list
                Type type;
                if ((type = value.Method.GetDeclaringTypes().FirstOrDefault(x => typeof(Trigger).IsAssignableFrom(x))) != null)
                {
                    Application.Current.Dispatcher.BeginInvoke((Action) (() =>
                        {
                            var trigger = App.Triggers.First(x => x.GetType() == type);
                            _resultHistory.Add(trigger);
                            _result.Add(trigger);
                            OnPropertyChanged("ResultHistory");
                            OnPropertyChanged("Result");
                        }));
                }

                OnPropertyChanged();
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                ActivityResult handler2;
                var log = _resulted;
                do
                {
                    handler2 = log;
                    var handler3 = (ActivityResult)Delegate.Remove(handler2, value);
                    log = Interlocked.CompareExchange(ref _resulted, handler3, handler2);
                }
                while (log != handler2);

                // remove all entries from the result tracking
                Type type;
                if ((type = value.Method.GetDeclaringTypes().FirstOrDefault(x => typeof(Trigger).IsAssignableFrom(x))) != null)
                {
                    Application.Current.Dispatcher.BeginInvoke((Action) (() =>
                        {
                            var trigger = App.Triggers.First(x => x.GetType() == type);
                            _result.RemoveAll(x => x == trigger);
                            OnPropertyChanged("Result");
                        }));
                }

                OnPropertyChanged();
            }
        }

        protected void OnResulted(object obj, ActivityQueue queue)
        {
            var handler = _resulted;
            if (handler != null) _resulted(obj, queue, this);
        }

        [Category("Results")]
        [Description(
            "Some activities result in a trigger being fired on top of the current activity executing.  This section lists the triggers that fire when an activity is executed."
            )]
        [Editor(typeof (PrimitiveTypeCollectionEditor), typeof (PrimitiveTypeCollectionEditor))]
        public List<Trigger> Result
        {
            get
            {
                /*if(_resulted == null)
                    return new List<Trigger>();
                var subs = _resulted.GetInvocationList();
                var triggers = subs.Select(x => x.Method.DeclaringType).Where(x => typeof (Trigger).IsAssignableFrom(x));
                return App.Triggers.Where(x => triggers.Contains(x.GetType())).ToList();*/
                return _result;
            }
        }

        protected Activity(Life.Activity activity)
        {
            _activity = activity;
        }

        public Life.Activity Entity
        {
            get { return _activity; }
            internal set { _activity = value; }
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
            if (arg is T)
                return (T)arg;
            return default(T);
        }

        public abstract void Execute(object context, ActivityQueue queue, Trigger trigger);
    }
}