using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Life.Utilities.Extensions;
using QuickGraph;
using QuickGraph.Algorithms.Search;
using QuickGraph.Collections;

namespace Life.Controls
{
    public class Navigator : VirtualizingPanel
    {
        #region "Constants"
        private const double LAYER_GAP = 20;
        private const double VERTEX_GAP = 5;
        #endregion

        #region "Fields"
        internal Point TopLeft = new Point(0, 0);
        private readonly Semaphore _waiter = new Semaphore(1, 1);
        private readonly BidirectionalGraph<FrameworkElement, Edge> _graph;

        private BackgroundWorker _worker;
        private Point _bottomRight;
        #endregion

        #region "DependencyProperties"
        public static readonly RoutedEvent PositionChangedEvent = EventManager.RegisterRoutedEvent(
            "PositionChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(Navigator)
        );

        public static readonly DependencyProperty XProperty =
            DependencyProperty.RegisterAttached(
                "X",
                typeof (double),
                typeof (Navigator),
                new FrameworkPropertyMetadata(double.NaN,
                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                              FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                                              FrameworkPropertyMetadataOptions.AffectsParentArrange |
                                              FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                              OnPositionChanged));

        private static void OnPositionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var vertex = sender as FrameworkElement;
            if (vertex == null) return;
            vertex.RaiseEvent(new RoutedEventArgs(PositionChangedEvent, vertex));
        }


        public static readonly DependencyProperty YProperty =
            DependencyProperty.RegisterAttached(
                "Y",
                typeof (double),
                typeof (Navigator),
                new FrameworkPropertyMetadata(double.NaN,
                                              FrameworkPropertyMetadataOptions.AffectsMeasure |
                                              FrameworkPropertyMetadataOptions.AffectsArrange |
                                              FrameworkPropertyMetadataOptions.AffectsRender |
                                              FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                                              FrameworkPropertyMetadataOptions.AffectsParentArrange |
                                              FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                              OnPositionChanged));

        public static readonly DependencyProperty ParentProperty = DependencyProperty.RegisterAttached(
            "Parent",
            typeof (FrameworkElement),
            typeof (Navigator),
            new FrameworkPropertyMetadata(null,
                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                          FrameworkPropertyMetadataOptions.AffectsRender |
                                          FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsParentArrange,
                                          OnParentChanged));

        private static void OnParentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // TODO: target which controls need to be updated
            var navigator = sender.FindParent<Navigator>();
            if (navigator == null) return;
            var oldEdge = navigator._graph.Edges.FirstOrDefault(x =>
                                                                Equals(x.Source, e.OldValue) &&
                                                                Equals(x.Target, sender));
            if (oldEdge != null)
            {
                navigator._graph.RemoveEdge(oldEdge);
                navigator.InternalChildren.Remove(oldEdge);
            }
            var newEdge = new Edge((FrameworkElement) e.NewValue, (FrameworkElement) sender, navigator);
            navigator._graph.AddEdge(newEdge);
            navigator.InternalChildren.Add(newEdge);
            navigator.Layout();
        }

        public static readonly DependencyProperty TargetsProperty = DependencyProperty.RegisterAttached(
            "Targets",
            typeof (ItemCollection),
            typeof (Navigator),
            new FrameworkPropertyMetadata(null,
                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                          FrameworkPropertyMetadataOptions.AffectsRender |
                                          FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsParentArrange,
                                          OnTargetsChanged));

        private RoutedEventHandler _handler;

        private static void OnTargetsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // TODO: target which controls need to be updated
            var navigator = sender.FindParent<Navigator>();
            if (navigator != null)
                navigator.Layout();
        }

        #endregion

        #region "Properties"
        public static void SetTargets(UIElement element, ItemCollection value)
        {
            element.SetValue(TargetsProperty, value);
        }

        public static ItemCollection GetTargets(FrameworkElement element)
        {
            return (ItemCollection)element.GetValue(TargetsProperty);
        }

        public static void SetParent(UIElement element, FrameworkElement value)
        {
            element.SetValue(ParentProperty, value);
        }

        public static FrameworkElement GetParent(FrameworkElement element)
        {
            return (FrameworkElement)element.GetValue(ParentProperty);
        }

        #endregion

        #region "Constructors"

        public Navigator()
        {
            //Vertices.CollectionChanged += (sender, args) => UpdateGraph();
            //Edges.CollectionChanged += (sender, args) => UpdateGraph();
            _graph = new BidirectionalGraph<FrameworkElement, Edge>(true);
            // add edges and vertices
            foreach (var child in InternalChildren.OfType<FrameworkElement>().Where(x => !(x is Edge)).ToList())
            {
                if (_graph.AddVertex(child))
                {
                    child.AddHandler(SizeChangedEvent, (RoutedEventHandler)Layout);
                    child.IsVisibleChanged += Layout;
                }
                var parent = child.GetValue(ParentProperty);
                if (parent != null)
                {
                    var newEdge = new Edge(child, (FrameworkElement) parent, this);
                    _graph.AddEdge(newEdge);
                }
            }
            SizeChanged += Layout;
        }

        #endregion

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            var vertex = visualAdded as FrameworkElement;
            if (vertex != null && !(vertex is Edge))
            {
                if (_graph.AddVertex(vertex))
                {
                    vertex.AddHandler(SizeChangedEvent, (RoutedEventHandler) Layout);
                    vertex.IsVisibleChanged += Layout;
                    var parent = vertex.GetValue(ParentProperty);
                    if (parent != null)
                    {
                        var newEdge = new Edge(vertex, (FrameworkElement)parent, this);
                        _graph.AddEdge(newEdge);
                    }
                }
            }

            vertex = visualRemoved as FrameworkElement;
            if (vertex != null && !(vertex is Edge))
            {
                if (_graph.RemoveVertex(vertex))
                {
                    vertex.RemoveHandler(SizeChangedEvent, (RoutedEventHandler) Layout);
                    vertex.IsVisibleChanged -= Layout;
                    var edgesToRemove = _graph.Edges.Where(x => x.Source == vertex || x.Target == vertex).ToList();
                    foreach (var edge in edgesToRemove)
                    {
                        _graph.RemoveEdge(edge);
                        InternalChildren.Remove(edge);
                    }
                }
            }
        }

        private void Layout(object sender, DependencyPropertyChangedEventArgs e)
        {
            Layout();
        }

        private void Layout(object sender, EventArgs e) 
        {
            Layout();
        }

        private void Layout()
        {
            if (_graph == null || _graph.VertexCount == 0)
                return; //no graph to layout, or wrong layout algorithm

            if (!IsLoaded)
            {
                if (_handler == null)
                {
                    _handler = (s, e) =>
                        {
                            Layout();
                            var gl = (Navigator) e.Source;
                            gl.Loaded -= _handler;
                        };
                }
                Loaded += _handler;
                return;
            }

            //Asynchronous computing - progress report & anything else
            //if there's a running progress than cancel that
            if (_worker != null && _worker.IsBusy && _worker.WorkerSupportsCancellation)
                _worker.CancelAsync();

            if (!_waiter.WaitOne(0))
                return;

            _worker = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true
                };


            var sizes = _graph.Vertices.ToDictionary(x => x,
                                                     x => x.DesiredSize);

            var positions = _graph.Vertices.ToDictionary(x => x,
                                                         x =>
                                                         x.TranslatePoint(TopLeft, this) +
                                                         new Vector((double) x.GetValue(ActualWidthProperty),
                                                                    (double) x.GetValue(ActualHeightProperty)));
            var graph = _graph;
            //run the algorithm on a background thread
            _worker.DoWork += ((sender, e) =>
                {
                    ComputeTree(true, false, graph, positions, sizes);
                    e.Result = positions;
                });

            //progress changed if an iteration ended
            _worker.ProgressChanged +=
                ((s, e) =>
                    {
                    });

            //background thread finished if the iteration ended
            _worker.RunWorkerCompleted += ((s, e) =>
                {
                    Dispatcher.BeginInvoke((Action) (() =>
                        {
                            foreach (var pos in (Dictionary<FrameworkElement, Point>) e.Result)
                            {
                                Animate(pos.Key, pos.Value.X, pos.Value.Y, new TimeSpan(0, 0, 0, 0, 500));
                            }

                        }));
                    //OnLayoutFinished();
                    _worker = null;
                    _waiter.Release();
                });

            //OnLayoutStarted();
            _worker.RunWorkerAsync();
        }

        /*
        TODO: finish this
        protected IDictionary<Edge, Point[]> RouteEdges(IDictionary<FrameworkElement, Point> positions,
                                                         IDictionary<FrameworkElement, Size> sizes)
        {

        }
        */

        private void Animate(FrameworkElement vertex, double x, double y, TimeSpan duration)
        {
            if (!double.IsNaN(x))
            {
                var from = (double)vertex.GetValue(XProperty);
                from = double.IsNaN(from) ? 0.0 : from;

                //create the animation for the horizontal position
                var animationX = new DoubleAnimation(
                    from,
                    x,
                    duration,
                    FillBehavior.HoldEnd);
                animationX.Completed += (s, e) =>
                {
                    vertex.BeginAnimation(XProperty, null);
                    vertex.SetValue(XProperty, x);
                };
                vertex.BeginAnimation(XProperty, animationX, HandoffBehavior.Compose);
            }
            if (double.IsNaN(y)) return;
            {
                var from = (double)vertex.GetValue(YProperty);
                @from = (double.IsNaN(@from) ? 0.0 : @from);

                //create an animation for the vertical position
                var animationY = new DoubleAnimation(
                    @from, y,
                    duration,
                    FillBehavior.HoldEnd);
                animationY.Completed += (s, e) =>
                {
                    vertex.BeginAnimation(YProperty, null);
                    vertex.SetValue(YProperty, y);
                };
                vertex.BeginAnimation(YProperty, animationY, HandoffBehavior.Compose);
            }
        }

        /*
        private void Fade(
            Control control,
            TimeSpan duration,
            Action<Control> endMethod)
        {
            var storyboard = new Storyboard();

            DoubleAnimation fadeAnimation;

            if (rounds > 1)
            {
                fadeAnimation = new DoubleAnimation(startOpacity, endOpacity, new Duration(duration));
                fadeAnimation.AutoReverse = true;
                fadeAnimation.RepeatBehavior = new RepeatBehavior(rounds - 1);
                storyboard.Children.Add(fadeAnimation);
                Storyboard.SetTarget(fadeAnimation, control);
                Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(UIElement.OpacityProperty));
            }

            fadeAnimation = new DoubleAnimation(startOpacity, endOpacity, new Duration(duration));
            fadeAnimation.BeginTime = TimeSpan.FromMilliseconds(duration.TotalMilliseconds * (rounds - 1) * 2);
            storyboard.Children.Add(fadeAnimation);
            Storyboard.SetTarget(fadeAnimation, control);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(UIElement.OpacityProperty));

            if (endMethod != null)
                storyboard.Completed += (s, a) => endMethod(control);
            storyboard.Begin(control);
        }
        */

        private void ComputeTree(bool horizontal, bool reverse, BidirectionalGraph<FrameworkElement, Edge> graph, Dictionary<FrameworkElement, Point> positions, Dictionary<FrameworkElement, Size> sizes)
        {
            if (horizontal)
            {
                //change the sizes
                foreach (var sizePair in sizes.ToArray())
                    sizes[sizePair.Key] = new Size(sizePair.Value.Height, sizePair.Value.Width);
            }

            var spanningTree = GenerateSpanningTree(graph);
            //DoWidthAndHeightOptimization();

            var layers = new List<Layer>();
            var data = new Dictionary<FrameworkElement, VertexData>();

            //first layout the vertices with 0 in-edge
            foreach (var source in spanningTree.Vertices.Where(v => spanningTree.InDegree(v) == 0).ToList())
                CalculatePosition(source, null, 0, sizes, ref layers, ref data, spanningTree);

            //then the others
            foreach (var source in spanningTree.Vertices.ToList())
                CalculatePosition(source, null, 0, sizes, ref layers, ref data, spanningTree);

            AssignPositions(horizontal, reverse, sizes, positions, ref layers, ref data);
        }

        private static void NormalizePositions(IDictionary<FrameworkElement, Point> vertexPositions)
        {
            if (vertexPositions == null || vertexPositions.Count == 0)
                return;

            //get the topLeft position
            var topLeft = new Point(float.PositiveInfinity, float.PositiveInfinity);
            foreach (var pos in vertexPositions.Values.ToArray())
            {
                topLeft.X = Math.Min(topLeft.X, pos.X);
                topLeft.Y = Math.Min(topLeft.Y, pos.Y);
            }

            //translate with the topLeft position
            foreach (var v in vertexPositions.Keys.ToArray())
            {
                var pos = vertexPositions[v];
                pos.X -= topLeft.X;
                pos.Y -= topLeft.Y;
                vertexPositions[v] = pos;
            }
        }


        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var translate = new Vector(-TopLeft.X, -TopLeft.Y);
            var graphSize = (_bottomRight - TopLeft);

            if (double.IsNaN(graphSize.X) || double.IsNaN(graphSize.Y) ||
                 double.IsInfinity(graphSize.X) || double.IsInfinity(graphSize.Y))
                translate = new Vector(0, 0);

            //Translation = translate;

            graphSize = _graph.VertexCount > 0
                            ? new Vector(double.NegativeInfinity, double.NegativeInfinity)
                            : new Vector(0, 0);

            //translate with the topLeft
            foreach (var child in _graph.Vertices)
            {
                var x = (double)child.GetValue(XProperty);
                var y = (double)child.GetValue(YProperty);
                if (double.IsNaN(x) || double.IsNaN(y))
                {
                    //not a vertex, set the coordinates of the top-left corner
                    x = double.IsNaN(x) ? translate.X : x;
                    y = double.IsNaN(y) ? translate.Y : y;
                }
                else
                {
                    //this is a vertex
                    x += translate.X;
                    y += translate.Y;

                    //get the top-left corner
                    x -= child.DesiredSize.Width * 0.5;
                    y -= child.DesiredSize.Height * 0.5;
                }
                child.Arrange(new Rect(new Point(x, y), child.DesiredSize));

                graphSize.X = Math.Max(0, Math.Max(graphSize.X, x + child.DesiredSize.Width));
                graphSize.Y = Math.Max(0, Math.Max(graphSize.Y, y + child.DesiredSize.Height));
            }

            return new Size(graphSize.X, graphSize.Y);
        }

        /// <summary>
        /// Overridden measure. It calculates a size where all of 
        /// of the vertices are visible.
        /// </summary>
        /// <param name="constraint">The size constraint.</param>
        /// <returns>The calculated size.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            TopLeft = new Point(double.PositiveInfinity, double.PositiveInfinity);
            _bottomRight = new Point(double.NegativeInfinity, double.NegativeInfinity);

            foreach (var child in _graph.Vertices)
            {
                //measure the child
                child.Measure(constraint);

                //get the position of the vertex
                var left = (double)child.GetValue(XProperty);
                var top = (double)child.GetValue(YProperty);

                var halfWidth = child.DesiredSize.Width * 0.5;
                var halfHeight = child.DesiredSize.Height * 0.5;

                if (double.IsNaN(left) || double.IsNaN(top))
                {
                    left = halfWidth;
                    top = halfHeight;
                }

                //get the top left corner point
                TopLeft.X = Math.Min(TopLeft.X, left - halfWidth);
                TopLeft.Y = Math.Min(TopLeft.Y, top - halfHeight);

                //calculate the bottom right corner point
                _bottomRight.X = Math.Max(_bottomRight.X, left + halfWidth);
                _bottomRight.Y = Math.Max(_bottomRight.Y, top + halfHeight);
            }

            var graphSize = (Size)(_bottomRight - TopLeft);
            graphSize.Width = Math.Max(0, graphSize.Width);
            graphSize.Height = Math.Max(0, graphSize.Height);

            if (double.IsNaN(graphSize.Width) || double.IsNaN(graphSize.Height) ||
                 double.IsInfinity(graphSize.Width) || double.IsInfinity(graphSize.Height))
                return new Size(0, 0);

            return graphSize;
        }

        private BidirectionalGraph<FrameworkElement, Edge> GenerateSpanningTree(BidirectionalGraph<FrameworkElement, Edge> graph)
        {
            var spanningTree = new BidirectionalGraph<FrameworkElement, Edge>(false);
            spanningTree.AddVertexRange(graph.Vertices.ToList());
            IQueue<FrameworkElement> vb = new QuickGraph.Collections.Queue<FrameworkElement>();
            vb.Enqueue(graph.Vertices.OrderBy(graph.InDegree).First());
            //switch (Parameters.SpanningTreeGeneration)
            //{
            //    case SpanningTreeGeneration.BFS:
            var bfsAlgo = new DepthFirstSearchAlgorithm<FrameworkElement, Edge>(graph);
                    bfsAlgo.TreeEdge += e => spanningTree.AddEdge(e);
                    bfsAlgo.Compute();
            //        break;
            //    case SpanningTreeGeneration.DFS:
            //        var dfsAlgo = new DepthFirstSearchAlgorithm<TVertex, TEdge>(VisitedGraph);
            //        dfsAlgo.TreeEdge += e => spanningTree.AddEdge(new Edge<TVertex>(e.Source, e.Target));
            //        dfsAlgo.Compute();
            //        break;
            //}
            return spanningTree;
        }

        private double CalculatePosition(FrameworkElement v, FrameworkElement parent, int l, Dictionary<FrameworkElement, Size> sizes, ref List<Layer> layers, ref Dictionary<FrameworkElement, VertexData> data, BidirectionalGraph<FrameworkElement, Edge> spanningTree)
        {
            if (data.ContainsKey(v))
                return -1; //this vertex is already layed out

            while (l >= layers.Count)
                layers.Add(new Layer());

            var layer = layers[l];
            var size = sizes[v];
            var d = new VertexData { Parent = parent };
            data[v] = d;

            layer.NextPosition += size.Width / 2.0;
            if (l > 0)
            {
                layer.NextPosition += layers[l - 1].LastTranslate;
                layers[l - 1].LastTranslate = 0;
            }
            layer.Size = Math.Max(layer.Size, size.Height + LAYER_GAP);
            layer.Vertices.Add(v);
            if (spanningTree.OutDegree(v) == 0)
            {
                d.Position = layer.NextPosition;
            }
            else
            {
                var minPos = double.MaxValue;
                var maxPos = -double.MaxValue;
                //first put the children
                foreach (var child in spanningTree.OutEdges(v).Select(e => e.Target))
                {
                    var childPos = CalculatePosition(child, v, l + 1, sizes, ref layers, ref data, spanningTree);
                    if (!(childPos >= 0)) continue;
                    minPos = Math.Min(minPos, childPos);
                    maxPos = Math.Max(maxPos, childPos);
                }
                if (Math.Abs(minPos - double.MaxValue) > 0)
                    d.Position = (minPos + maxPos) / 2.0;
                else
                    d.Position = layer.NextPosition;
                d.Translate = Math.Max(layer.NextPosition - d.Position, 0);

                layer.LastTranslate = d.Translate;
                d.Position += d.Translate;
                layer.NextPosition = d.Position;
            }
            // TODO: replace layer gap with a property per vertex, that is each vertex can have its own gap
            layer.NextPosition += size.Width / 2.0 + VERTEX_GAP;

            return d.Position;
        }

        private void AssignPositions(bool horizontal, bool reverse, Dictionary<FrameworkElement, Size> sizes, Dictionary<FrameworkElement, Point> positions, ref List<Layer> layers, ref Dictionary<FrameworkElement, VertexData> data)
        {
            double layerSize = 0;

            foreach (var layer in layers)
            {
                foreach (var v in layer.Vertices)
                {
                    var size = sizes[v];
                    var d = data[v];
                    if (d.Parent != null)
                    {
                        d.Position += data[d.Parent].Translate;
                        d.Translate += data[d.Parent].Translate;
                    }

                    positions[v] =
                        horizontal
                            ? new Point((reverse ? -1 : 1)*(layerSize + size.Height/2.0), d.Position)
                            : new Point(d.Position, (reverse ? -1 : 1)*(layerSize + size.Height/2.0));
                }
                layerSize += layer.Size;
            }

            if (reverse)
                NormalizePositions(positions);
        }

        class Layer
        {
            public double Size;
            public double NextPosition;
            public readonly IList<FrameworkElement> Vertices = new List<FrameworkElement>();
            public double LastTranslate;

            public Layer()
            {
                LastTranslate = 0;
            }

            /* Width and Height Optimization */

        }

        class VertexData
        {
            public FrameworkElement Parent;
            public double Translate;
            public double Position;

            /* Width and Height Optimization */

        }

    }
}
