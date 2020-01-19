using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using QuickGraph;

namespace Life.Controls
{
    /// <summary>
    /// Interaction logic for Activity.xaml
    /// </summary>
    public partial class Activity
    {
        private readonly BidirectionalGraph<object, IEdge<object>> _internal =
            new BidirectionalGraph<object, IEdge<object>>();

        public Activity()
        {
            InitializeComponent();

            // check for triggers
            foreach (var trigger in App.Triggers)
                trigger.Triggered += Triggered;
            foreach (var activity in App.Activities)
                activity.PropertyChanged += ActivityOnPropertyChanged;
            App.Triggers.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                        foreach (var trigger in args.NewItems.OfType<Utilities.Trigger>())
                            trigger.Triggered += Triggered;
                };
            App.Activities.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                        foreach (var activity in args.NewItems.OfType<Utilities.Activity>())
                            activity.PropertyChanged += ActivityOnPropertyChanged;
                };
            App.ActivityCompleted += AppOnActivityCompleted;
        }

        private void AppOnActivityCompleted(Utilities.Activity activity)
        {
            // update list of recent activites
            Dispatcher.BeginInvoke((Action) (() =>
                {
                    using (var data = new DatalayerDataContext())
                    {
                        var recent = data.ActivityQueues
                                         .Where(x => x.TimeStarted >= App.Start && x.TimeCompleted != null &&
                                                     x.TimeAdded >= App.Start)
                                         .ToList();
                        var activities =
                            recent.Select(x => App.Activities.First(y => y.Entity.Id == x.ActivityId))
                                  .ToList();
                        var oldItems =
                            Recent.Children.OfType<ContentPresenter>()
                                  .Where(x => !activities.Contains(x.Content))
                                  .ToList();
                        var newItems = activities.Where(x => !Recent.Children
                                                                    .OfType<ContentPresenter>()
                                                                    .Select(y => y.Content).Contains(x))
                                                 .ToList();
                        foreach (var old in oldItems)
                            Recent.Children.Remove(old);
                        foreach (var newItem in newItems)
                            Recent.Children.Add(new ContentPresenter
                                {
                                    Content = newItem,
                                    Margin = new Thickness(5)
                                });
                    }
                }), DispatcherPriority.ApplicationIdle);
        }

        private void ActivityOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var activity = sender as Utilities.Activity;
            if (activity != null && e.PropertyName == "Resulted")
            {
                Dispatcher.BeginInvoke((Action) (() =>
                    {
                        _internal.AddVertex(activity);
                        var results = activity.Result;
                        var history = activity.ResultHistory.Except(results);
                        var oldItems =
                            _internal.Vertices.OfType<Utilities.Trigger>()
                                     .Where(x => !results.Contains(x) && !history.Contains(x))
                                     .ToList();
                        var newItems = activity.ResultHistory.Where(x => !_internal.Vertices.Contains(x))
                                               .ToList();
                        foreach (var newItem in newItems)
                            _internal.AddVertex(newItem);
                        foreach (var result in results)
                            _internal.AddEdge(new Edge<object>(activity, result));
                    }), DispatcherPriority.ApplicationIdle);
            }
        }

        private void Triggered(Utilities.Trigger trigger, object result)
        {
            // refresh recent list
            Dispatcher.BeginInvoke((Action) (() =>
                {
                    using (var data = new DatalayerDataContext())
                    {
                        var onGoing = data.ActivityQueues
                                          .Where(x => x.TimeStarted >= App.Start && x.TimeCompleted == null &&
                                                      x.TimeAdded >= App.Start)
                                          .ToList();
                        var activities =
                            onGoing.Select(x => App.Activities.First(y => y.Entity.Id == x.ActivityId))
                                   .ToList();
                        var oldItems =
                            Ongoing.Children.OfType<ContentPresenter>()
                                   .Where(x => !activities.Contains(x.Content))
                                   .ToList();
                        var newItems = activities.Where(x => !Ongoing.Children
                                                                     .OfType<ContentPresenter>()
                                                                     .Select(y => y.Content).Contains(x))
                                                 .ToList();
                        foreach (var old in oldItems)
                            Ongoing.Children.Remove(old);
                        foreach (var newItem in newItems)
                            Ongoing.Children.Add(new ContentPresenter
                                {
                                    Content = newItem,
                                    Margin = new Thickness(5)
                                });
                    }
                }), DispatcherPriority.ApplicationIdle);
        }

        private void Parameters_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).PropertyGrid.SelectedObject = ((Border) sender).DataContext;

            ((MainWindow) Application.Current.MainWindow).PropertyGrid.PropertyDefinitions.Clear();
            var trigger = ((Border) sender).DataContext as Utilities.Trigger;
            if (trigger != null)
            {
                foreach (var prop in trigger.Properties)
                    ((MainWindow) Application.Current.MainWindow).PropertyGrid.PropertyDefinitions.Add(prop);
            }

            var activity = ((Border) sender).DataContext as Utilities.Activity;
            if (activity == null) return;
            foreach (var prop in activity.Properties)
                ((MainWindow) Application.Current.MainWindow).PropertyGrid.PropertyDefinitions.Add(prop);
        }
    }
}
