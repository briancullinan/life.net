using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using QuickGraph;

namespace Life.Controls
{
    public class Edge : Control, IEdge<FrameworkElement>
    {
        public event RoutedEventHandler EdgeChanged;

        public Edge(FrameworkElement source, FrameworkElement target, Navigator navigator)
        {
            DataContext = this;

            Source = source;
            Target = target;
            _navigator = navigator;

            source.AddHandler(Navigator.PositionChangedEvent, (RoutedEventHandler)OnEdgeChanged);
            target.AddHandler(Navigator.PositionChangedEvent, (RoutedEventHandler)OnEdgeChanged);
        }

        private void OnEdgeChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            //get the position of the source
            var sourcePos = new Point
            {
                X = (double)Source.GetValue(Navigator.XProperty),
                Y = (double)Source.GetValue(Navigator.YProperty)
            };
            //get the size of the source
            var sourceSize = new Size
            {
                Width = Source.ActualWidth,
                Height = Source.ActualHeight
            };
            //get the position of the target
            var targetPos = new Point
            {
                X = (double)Target.GetValue(Navigator.XProperty),
                Y = (double)Target.GetValue(Navigator.YProperty)
            };
            //get the size of the target
            var targetSize = new Size
            {
                Width = Target.ActualWidth,
                Height = Target.ActualHeight
            };

            //get the route informations
            Point[] routeInformation = null;

            var hasRouteInfo = routeInformation != null && routeInformation.Length > 0;

            //
            // Create the path
            //
            var p1 = CalculateAttachPoint(sourcePos, sourceSize, (hasRouteInfo ? routeInformation[0] : targetPos));
            var p2 = CalculateAttachPoint(targetPos, targetSize,
                                            (hasRouteInfo
                                                 ? routeInformation[routeInformation.Length - 1]
                                                 : sourcePos));
            if (double.IsNaN(p1.X) || double.IsNaN(p1.Y) || double.IsNaN(p2.X) || double.IsNaN(p2.Y))
                return;

            var segments = new PathSegment[1 + (hasRouteInfo ? routeInformation.Length : 0)];
            if (hasRouteInfo)
                //append route points
                for (int i = 0; i < routeInformation.Length; i++)
                    segments[i] = new LineSegment(routeInformation[i], true);

            var pLast = (hasRouteInfo ? routeInformation[routeInformation.Length - 1] : p1);
            var v = pLast - p2;
            v = v / v.Length * 5;
            var n = new Vector(-v.Y, v.X) * 0.3;

            if (double.IsNaN(n.X) || double.IsNaN(n.Y))
                return;

            segments[segments.Length - 1] = new LineSegment(p2 + v, true);

            var pfc = new PathFigureCollection(2)
                {
                    new PathFigure(p1, segments, false),
                    new PathFigure(p2,
                                   new PathSegment[]
                                       {
                                           new LineSegment(p2 + v - n, true),
                                           new LineSegment(p2 + v + n, true)
                                       }, true)
                };

            Data = new PathGeometry(pfc);
            Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Arrange(new Rect(DesiredSize));
            //Arrange(new Rect(_navigator.TopLeft, _navigator.DesiredSize));

            if (EdgeChanged != null)
                EdgeChanged(this, new RoutedEventArgs());
        }

        public FrameworkElement Source { get; private set; }

        public FrameworkElement Target { get; private set; }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data",
            typeof (Geometry),
            typeof (Edge),
            new FrameworkPropertyMetadata(null,
                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                          FrameworkPropertyMetadataOptions.AffectsRender |
                                          FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsParentArrange,
                                          null));

        private Navigator _navigator;

        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static Point CalculateAttachPoint(Point s, Size sourceSize, Point t)
        {
            double[] sides = new double[4];
            sides[0] = (s.X - sourceSize.Width / 2.0 - t.X) / (s.X - t.X);
            sides[1] = (s.Y - sourceSize.Height / 2.0 - t.Y) / (s.Y - t.Y);
            sides[2] = (s.X + sourceSize.Width / 2.0 - t.X) / (s.X - t.X);
            sides[3] = (s.Y + sourceSize.Height / 2.0 - t.Y) / (s.Y - t.Y);

            double fi = 0;
            for (int i = 0; i < 4; i++)
            {
                if (sides[i] <= 1)
                    fi = Math.Max(fi, sides[i]);
            }

            return t + fi * (s - t);
        }
    }
}