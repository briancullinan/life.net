using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Life.Annotations;
using TimelineLibrary;

namespace Life.Controls
{
    /// <summary>
    /// Interaction logic for Timeline.xaml
    /// </summary>
    public partial class Timeline
    {
        private static Timer _timer;

        public Timeline()
        {
            InitializeComponent();
            if(_timer == null)
                _timer = new Timer(UpdateTimelineNow, null, 0, 1000);
        }

        private void UpdateTimelineNow(object state)
        {
            Dispatcher.BeginInvoke((Action) (() =>
                {
                    if (IsLoaded)
                        CurrentDateTime = DateTime.Now;
                }));
        }

        private void Timeline_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Alt)
            {
                e.Handled = true;
                var zoomIn = e.Delta > 0;
                var bands = zoomIn 
                    ? Children.OfType<TimelineBand>().Reverse().ToList()
                    : Children.OfType<TimelineBand>().ToList();
                var rows = zoomIn
                               ? RowDefinitions.Reverse().ToList()
                               : RowDefinitions.ToList();
                var index = bands.IndexOf(bands.First(x => x.IsMainBand));
                var amount = Math.Min(Math.Abs(e.Delta / 120), bands.Count - index - 1);
                if (amount > 0)
                {
                    var main = bands.SkipWhile(x => !x.IsMainBand).First();
                    var next = bands.SkipWhile(x => !x.IsMainBand).Skip(amount).First();
                    main.IsMainBand = false;
                    next.IsMainBand = true;
                    main.MaxEventHeight = 130;
                    foreach (var band in bands.TakeWhile(x => !x.IsMainBand))
                        band.MaxEventHeight = 4;
                    var mainRow = rows.SkipWhile(x => x.Height != new GridLength(1, GridUnitType.Star)).First();
                    var nextRow =
                        rows.SkipWhile(x => x.Height != new GridLength(1, GridUnitType.Star))
                            .Skip(amount)
                            .First();
                    mainRow.Height = new GridLength(40);
                    nextRow.Height = new GridLength(1, GridUnitType.Star);
                    foreach (var hide in rows.TakeWhile(x => x.Height != new GridLength(1, GridUnitType.Star)))
                        hide.Height = new GridLength(40);

                    // reset the bands
                    HookChildElements(Children);
                    main.CreateTimelineCalculator(
                        CalendarType,
                        CurrentDateTime,
                        MinDateTime,
                        MaxDateTime
                        );

                    bands.ForEach(
                        b => b.CreateTimelineCalculator(CalendarType, CurrentDateTime, MinDateTime, MaxDateTime));
                    RefreshEvents(false);
                }
            }
        }
    }
}
