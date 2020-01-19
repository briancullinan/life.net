﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using QuickGraph;

namespace Life.Controls
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        public Settings()
        {
            InitializeComponent();
        }

        private readonly BidirectionalGraph<object, IEdge<object>> _graph = new BidirectionalGraph<object, IEdge<object>>();
        public static readonly DependencyProperty ActivityAndTriggersProperty = DependencyProperty.Register(
            "ActivitiesAndTriggers",
            typeof(List<Type>), 
            typeof (Settings), 
            new PropertyMetadata(null));

        public List<Type> ActivitiesAndTriggers
        {
            get { return (List<Type>) GetValue(ActivityAndTriggersProperty); }
            set { SetValue(ActivityAndTriggersProperty, value); }
        }

        private void CreateVertices()
        {
            // load activities and triggers here
            ActivitiesAndTriggers = App.Plugins
                                       .SelectMany(x => x.GetTypes()
                                                         .Where(
                                                             y =>
                                                             typeof (Utilities.Trigger).IsAssignableFrom(y) ||
                                                             typeof (Utilities.Activity).IsAssignableFrom(y)))
                                       .ToList();
            using (var data = new DatalayerDataContext())
            {
                foreach (var edge in data.ActivityTriggers)
                {
                    var source = App.Triggers.FirstOrDefault(x => x.Entity.Id == edge.TriggerId);
                    var target = App.Activities.FirstOrDefault(x => x.Entity.Id == edge.ActivityId);
                    if (source == null || target == null) continue;
                    _graph.AddEdge(new Edge<object>(source, target));
                }
            }
        }

        private void TriggersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => TriggersOnCollectionChanged(sender, args));
                return;
            }

            if (args.NewItems != null)
                foreach (var item in args.NewItems)
                    _graph.AddVertex(item);
            if (args.OldItems != null)
                foreach (var item in args.OldItems)
                    _graph.RemoveVertex(item);
            CreateVertices();
        }

        private void ActivitiesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => ActivitiesOnCollectionChanged(sender, args));
                return;
            }

            if (args.NewItems != null)
                foreach (var item in args.NewItems)
                    _graph.AddVertex(item);
            if (args.OldItems != null)
                foreach (var item in args.OldItems)
                    _graph.RemoveVertex(item);
            CreateVertices();
        }

        private void Parameters_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).PropertyGrid.SelectedObject = ((Border)sender).DataContext;

            ((MainWindow)Application.Current.MainWindow).PropertyGrid.PropertyDefinitions.Clear();
            var trigger = ((Border) sender).DataContext as Utilities.Trigger;
            if (trigger != null)
            {
                foreach(var prop in trigger.Properties)
                    ((MainWindow)Application.Current.MainWindow).PropertyGrid.PropertyDefinitions.Add(prop);
            }

            var activity = ((Border)sender).DataContext as Utilities.Activity;
            if (activity == null) return;
            foreach (var prop in activity.Properties)
                ((MainWindow)Application.Current.MainWindow).PropertyGrid.PropertyDefinitions.Add(prop);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var type = (Type)Types.SelectedItem;
            if (typeof (Utilities.Trigger).IsAssignableFrom(type))
            {
                var newTrigger = (Utilities.Trigger) FormatterServices.GetSafeUninitializedObject(type);
                newTrigger.Entity = new Trigger
                    {
                        Enabled = false,
                        Type = type.FullName
                    };
                App.AppContext.Triggers.InsertOnSubmit(newTrigger.Entity);
                App.Triggers.Add(newTrigger);
            }
            if (!typeof (Utilities.Activity).IsAssignableFrom(type)) return;
            var newActivity = (Utilities.Activity)FormatterServices.GetSafeUninitializedObject(type);
            newActivity.Entity = new Life.Activity
                {
                    Enabled = false,
                    Type = type.FullName
                };
            App.AppContext.Activities.InsertOnSubmit(newActivity.Entity);
            App.Activities.Add(newActivity);
        }
    }
}