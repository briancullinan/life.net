﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Interactivity;
using System.Windows.Input;
using System.Collections.Generic;

namespace iLife.Utilities.Behaviors
{
    /// <summary>
    /// Based on Greg Schechter's Planeator
    /// http://blogs.msdn.com/b/greg_schechter/archive/2007/10/26/enter-the-planerator-dead-simple-3d-in-wpf-with-a-stupid-name.aspx
    /// </summary>


    [ContentProperty("Child")]
    public class Planerator : FrameworkElement
    {
        #region Public API

        #region Dependency Properties

        public static readonly DependencyProperty RotationXProperty =
            DependencyProperty.Register("RotationX", typeof(double), typeof(Planerator), new UIPropertyMetadata(0.0, (d, args) => ((Planerator)d).UpdateRotation()));
        public static readonly DependencyProperty RotationYProperty =
            DependencyProperty.Register("RotationY", typeof(double), typeof(Planerator), new UIPropertyMetadata(0.0, (d, args) => ((Planerator)d).UpdateRotation()));
        public static readonly DependencyProperty RotationZProperty =
            DependencyProperty.Register("RotationZ", typeof(double), typeof(Planerator), new UIPropertyMetadata(0.0, (d, args) => ((Planerator)d).UpdateRotation()));
        public static readonly DependencyProperty FieldOfViewProperty =
            DependencyProperty.Register("FieldOfView", typeof(double), typeof(Planerator), new UIPropertyMetadata(45.0, (d, args) => ((Planerator)d).Update3D(),
                (d, val) => Math.Min(Math.Max((double)val, 0.5), 179.9))); // clamp to a meaningful range


        public double RotationX
        {
            get { return (double)GetValue(RotationXProperty); }
            set { SetValue(RotationXProperty, value); }
        }
        public double RotationY
        {
            get { return (double)GetValue(RotationYProperty); }
            set { SetValue(RotationYProperty, value); }
        }
        public double RotationZ
        {
            get { return (double)GetValue(RotationZProperty); }
            set { SetValue(RotationZProperty, value); }
        }
        public double FieldOfView
        {
            get { return (double)GetValue(FieldOfViewProperty); }
            set { SetValue(FieldOfViewProperty, value); }
        }

        #endregion

        public FrameworkElement Child
        {
            get
            {
                return _originalChild;
            }
            set
            {
                if (!Equals(_originalChild, value))
                {
                    RemoveVisualChild(_visualChild);
                    RemoveLogicalChild(_logicalChild);

                    // Wrap child with special decorator that catches layout invalidations. 
                    _originalChild = value;
                    _logicalChild = new LayoutInvalidationCatcher { Child = _originalChild };
                    _visualChild = CreateVisualChild();

                    AddVisualChild(_visualChild);

                    // Need to use a logical child here to make sure databinding operations get down to it,
                    // since otherwise the child appears only as the Visual to a Viewport2DVisual3D, which 
                    // doesn't have databinding operations pass into it from above.
                    AddLogicalChild(_logicalChild);
                    InvalidateMeasure();
                }
            }
        }

        #endregion

        #region Layout Stuff

        protected override Size MeasureOverride(Size availableSize)
        {
            Size result;
            if (_logicalChild != null)
            {
                // Measure based on the size of the logical child, since we want to align with it.
                _logicalChild.Measure(availableSize);
                result = _logicalChild.DesiredSize;
                _visualChild.Measure(result);
            }
            else
            {
                result = new Size(0, 0);
            }
            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_logicalChild != null)
            {
                _logicalChild.Arrange(new Rect(finalSize));
                _visualChild.Arrange(new Rect(finalSize));
                Update3D();
            }
            return base.ArrangeOverride(finalSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visualChild;

        }

        protected override int VisualChildrenCount
        {
            get
            {
                return _visualChild == null ? 0 : 1;
            }
        }

        #endregion

        #region 3D Stuff

        private FrameworkElement CreateVisualChild()
        {
            var simpleQuad = new MeshGeometry3D
            {
                Positions = new Point3DCollection(Mesh),
                TextureCoordinates = new PointCollection(TexCoords),
                TriangleIndices = new Int32Collection(Indices)
            };

            // Front material is interactive, back material is not.
            Material frontMaterial = new DiffuseMaterial(Brushes.White);
            frontMaterial.SetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty, true);

            var vb = new VisualBrush(_logicalChild);
            SetCachingForObject(vb);  // big perf wins by caching!!
            Material backMaterial = new DiffuseMaterial(vb);

            _rotationTransform.Rotation = _quaternionRotation;
            var xfGroup = new Transform3DGroup { Children = { _scaleTransform, _rotationTransform } };

            var backModel = new GeometryModel3D { Geometry = simpleQuad, Transform = xfGroup, BackMaterial = backMaterial };
            var m3DGroup = new Model3DGroup
            {
                Children = { new DirectionalLight(Colors.White, new Vector3D(0, 0, -1)), 
                                 new DirectionalLight(Colors.White, new Vector3D(0.1, -0.1, 1)),
                                 backModel }
            };

            // Non-interactive Visual3D consisting of the backside, and two lights.
            var mv3D = new ModelVisual3D { Content = m3DGroup };

            // Interactive frontside Visual3D
            var frontModel = new Viewport2DVisual3D { Geometry = simpleQuad, Visual = _logicalChild, Material = frontMaterial, Transform = xfGroup };

            // Cache the brush in the VP2V3 by setting caching on it.  Big perf wins.
            SetCachingForObject(frontModel);

            // Scene consists of both the above Visual3D's.
            _viewport3D = new Viewport3D { ClipToBounds = false, Children = { mv3D, frontModel } };

            UpdateRotation();

            return _viewport3D;
        }

        private void SetCachingForObject(DependencyObject d)
        {
            RenderOptions.SetCachingHint(d, CachingHint.Cache);
            RenderOptions.SetCacheInvalidationThresholdMinimum(d, 0.5);
            RenderOptions.SetCacheInvalidationThresholdMaximum(d, 2.0);
        }

        private void UpdateRotation()
        {
            var qx = new Quaternion(XAxis, RotationX);
            var qy = new Quaternion(YAxis, RotationY);
            var qz = new Quaternion(ZAxis, RotationZ);

            _quaternionRotation.Quaternion = qx * qy * qz;
        }

        private void Update3D()
        {
            // Use GetDescendantBounds for sizing and centering since DesiredSize includes layout whitespace, whereas GetDescendantBounds 
            // is tighter
            var logicalBounds = VisualTreeHelper.GetDescendantBounds(_logicalChild);
            var w = logicalBounds.Width;
            var h = logicalBounds.Height;

            // Create a camera that looks down -Z, with up as Y, and positioned right halfway in X and Y on the element, 
            // and back along Z the right distance based on the field-of-view is the same projected size as the 2D content
            // that it's looking at.  See http://blogs.msdn.com/greg_schechter/archive/2007/04/03/camera-construction-in-parallaxui.aspx
            // for derivation of this camera.
            var fovInRadians = FieldOfView * (Math.PI / 180);
            var zValue = w / Math.Tan(fovInRadians / 2) / 2;
            _viewport3D.Camera = new PerspectiveCamera(new Point3D(w / 2, h / 2, zValue),
                                                       -ZAxis,
                                                       YAxis,
                                                       FieldOfView);


            _scaleTransform.ScaleX = w;
            _scaleTransform.ScaleY = h;
            _rotationTransform.CenterX = w / 2;
            _rotationTransform.CenterY = h / 2;
        }

        #endregion

        #region Private Classes

        /// <summary>
        /// Wrap this around a class that we want to catch the measure and arrange 
        /// processes occuring on, and propagate to the parent Planerator, if any.
        /// Do this because layout invalidations don't flow up out of a 
        /// Viewport2DVisual3D object.
        /// </summary>
        public class LayoutInvalidationCatcher : Decorator
        {
            public Planerator PlaParent { get { return Parent as Planerator; } }

            protected override Size MeasureOverride(Size constraint)
            {
                var pl = PlaParent;
                if (pl != null)
                {
                    pl.InvalidateMeasure();
                }
                return base.MeasureOverride(constraint);
            }

            protected override Size ArrangeOverride(Size arrangeSize)
            {
                var pl = PlaParent;
                if (pl != null)
                {
                    pl.InvalidateArrange();
                }
                return base.ArrangeOverride(arrangeSize);
            }
        }

        #endregion

        #region Private data

        // Instance data
        private FrameworkElement _logicalChild;
        private FrameworkElement _visualChild;
        private FrameworkElement _originalChild;

        private readonly QuaternionRotation3D _quaternionRotation = new QuaternionRotation3D();
        private readonly RotateTransform3D _rotationTransform = new RotateTransform3D();
        private Viewport3D _viewport3D;
        private readonly ScaleTransform3D _scaleTransform = new ScaleTransform3D();

        // Static data
        static private readonly Point3D[] Mesh = new[] { new Point3D(0, 0, 0), new Point3D(0, 1, 0), new Point3D(1, 1, 0), new Point3D(1, 0, 0) };
        static private readonly Point[] TexCoords = new[] { new Point(0, 1), new Point(0, 0), new Point(1, 0), new Point(1, 1) };
        static private readonly int[] Indices = new[] { 0, 2, 1, 0, 3, 2 };
        static private readonly Vector3D XAxis = new Vector3D(1, 0, 0);
        static private readonly Vector3D YAxis = new Vector3D(0, 1, 0);
        static private readonly Vector3D ZAxis = new Vector3D(0, 0, 1);

        #endregion
    }

    public class TiltBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty KeepDraggingProperty =
            DependencyProperty.Register("KeepDragging", typeof(bool), typeof(TiltBehavior), new PropertyMetadata(true));

        public bool KeepDragging
        {
            get { return (bool)GetValue(KeepDraggingProperty); }
            set { SetValue(KeepDraggingProperty, value); }
        }


        public static readonly DependencyProperty TiltFactorProperty =
            DependencyProperty.Register("TiltFactor", typeof(Int32), typeof(TiltBehavior), new PropertyMetadata(20));


        public Int32 TiltFactor
        {
            get { return (Int32)GetValue(TiltFactorProperty); }
            set { SetValue(TiltFactorProperty, value); }
        }

        private FrameworkElement _attachedElement;
        private Panel _originalPanel;
        private Thickness _originalMargin;
        private Size _originalSize;
        protected override void OnAttached()
        {
            _attachedElement = AssociatedObject;

            if (_attachedElement as Panel != null)
            {
                (_attachedElement as Panel).Loaded += (sl, el) =>
                {
                    var elements = new List<UIElement>();

                    var panel = _attachedElement as Panel;
                    if (panel != null)
                        elements.AddRange(panel.Children.Cast<UIElement>());
                    elements.ForEach(element => Interaction.GetBehaviors(element).Add(new TiltBehavior { KeepDragging = KeepDragging, TiltFactor = TiltFactor }));
                };

                return;
            }

            _originalPanel = _attachedElement.Parent as Panel;
            _originalMargin = _attachedElement.Margin;
            _originalSize = new Size(_attachedElement.Width, _attachedElement.Height);
            var left = Canvas.GetLeft(_attachedElement);
            var right = Canvas.GetRight(_attachedElement);
            var top = Canvas.GetTop(_attachedElement);
            var bottom = Canvas.GetBottom(_attachedElement);
            var z = Panel.GetZIndex(_attachedElement);
            var va = _attachedElement.VerticalAlignment;
            var ha = _attachedElement.HorizontalAlignment;

            #region Setting Container Properties
            RotatorParent = new Planerator
            {
                Margin = _originalMargin,
                Width = _originalSize.Width,
                Height = _originalSize.Height,
                VerticalAlignment = va,
                HorizontalAlignment = ha
            };
            RotatorParent.SetValue(Canvas.LeftProperty, left);
            RotatorParent.SetValue(Canvas.RightProperty, right);
            RotatorParent.SetValue(Canvas.TopProperty, top);
            RotatorParent.SetValue(Canvas.BottomProperty, bottom);
            RotatorParent.SetValue(Panel.ZIndexProperty, z);
            #endregion

            #region Removing Child Properties

            if (_originalPanel != null) _originalPanel.Children.Remove(_attachedElement);
            _attachedElement.Margin = new Thickness();
            _attachedElement.Width = double.NaN;
            _attachedElement.Height = double.NaN;
            #endregion

            if (_originalPanel != null) _originalPanel.Children.Add(RotatorParent);
            RotatorParent.Child = _attachedElement;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        public Planerator RotatorParent { get; set; }


        Point _current = new Point(-99, -99);
        bool _isPressed;
        Int32 _times = -1;
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (KeepDragging)
            {
                _current = Mouse.GetPosition(RotatorParent.Child);
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    if (_current.X > 0 && _current.X < _attachedElement.ActualWidth && _current.Y > 0 && _current.Y < _attachedElement.ActualHeight)
                    {
                        RotatorParent.RotationY = -1 * TiltFactor + _current.X * 2 * TiltFactor / _attachedElement.ActualWidth;
                        RotatorParent.RotationX = -1 * TiltFactor + _current.Y * 2 * TiltFactor / _attachedElement.ActualHeight;
                    }
                }
                else
                {
                    RotatorParent.RotationY = RotatorParent.RotationY - 5 < 0 ? 0 : RotatorParent.RotationY - 5;
                    RotatorParent.RotationX = RotatorParent.RotationX - 5 < 0 ? 0 : RotatorParent.RotationX - 5;
                }
            }
            else
            {

                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {

                    if (!_isPressed)
                    {
                        _current = Mouse.GetPosition(RotatorParent.Child);
                        if (_current.X > 0 && _current.X < _attachedElement.ActualWidth && _current.Y > 0 && _current.Y < _attachedElement.ActualHeight)
                        {
                            RotatorParent.RotationY = -1 * TiltFactor + _current.X * 2 * TiltFactor / _attachedElement.ActualWidth;
                            RotatorParent.RotationX = -1 * TiltFactor + _current.Y * 2 * TiltFactor / _attachedElement.ActualHeight;
                        }
                        _isPressed = true;
                    }


                    if (_isPressed && _times == 7)
                    {
                        RotatorParent.RotationY = RotatorParent.RotationY - 5 < 0 ? 0 : RotatorParent.RotationY - 5;
                        RotatorParent.RotationX = RotatorParent.RotationX - 5 < 0 ? 0 : RotatorParent.RotationX - 5;
                    }
                    else if (_isPressed && _times < 7)
                    {
                        _times++;
                    }
                }
                else
                {
                    _isPressed = false;
                    _times = -1;
                    RotatorParent.RotationY = RotatorParent.RotationY - 5 < 0 ? 0 : RotatorParent.RotationY - 5;
                    RotatorParent.RotationX = RotatorParent.RotationX - 5 < 0 ? 0 : RotatorParent.RotationX - 5;
                }

            }
        }
    }
}
