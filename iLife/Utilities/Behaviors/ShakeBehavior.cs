using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Interactivity;

namespace iLife.Utilities.Behaviors
{
    public class ShakeBehavior : Behavior<UIElement>
    {

        #region Declaration
        private UIElement _attachedElement;
        private static readonly Random ranNum = new Random();
        private int _currentAngle = 0;
        private int _currentDistant = 0;
        int _i;
        #endregion


        #region Property to Expose
        [Category("Animation Properties")]
        public int MaxRotationAngle
        {
            get { return (int)GetValue(MaxRotationAngleProperty); }
            set { SetValue(MaxRotationAngleProperty, value); }
        }

        public static readonly DependencyProperty MaxRotationAngleProperty = DependencyProperty.Register("Maximum Rotation Angle", typeof(int), typeof(ShakeBehavior), new PropertyMetadata(1));

        [Category("Animation Properties")]
        public int MaxMovmentDistant
        {
            get { return (int)GetValue(MaxMovmentDistantProperty); }
            set { SetValue(MaxMovmentDistantProperty, value); }
        }

        public static readonly DependencyProperty MaxMovmentDistantProperty = DependencyProperty.Register("Maximum Movment Distant", typeof(int), typeof(ShakeBehavior), new PropertyMetadata(3));

        #endregion

        protected override void OnAttached()
        {
            _attachedElement = AssociatedObject;

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            _i++;
            if (_i % 10 == 0)
            {
                var rt = new TransformGroup();
                var newAngle = new RotateTransform();
                var newPosition = new TranslateTransform();

                var objGeneralTransform = _attachedElement.TransformToVisual(Application.Current.MainWindow);
                var point = objGeneralTransform.Transform(new Point(0, 0));


                var oldTransform = _attachedElement.RenderTransform;
                if (oldTransform != null)
                {
                    rt.Children.Add(oldTransform);
                }

                var deltaAngle = ranNum.Next(-MaxRotationAngle - _currentAngle, (MaxRotationAngle + 1) - _currentAngle);
                newAngle.Angle = deltaAngle;
                _currentAngle = _currentAngle + deltaAngle;
                var deltaDistant = ranNum.Next(-MaxMovmentDistant - _currentDistant, (MaxMovmentDistant + 1) - _currentDistant);
                newPosition.X = deltaDistant;
                _currentDistant = _currentDistant + deltaDistant;
                newAngle.CenterX = point.X + _attachedElement.RenderSize.Width / 2;
                newAngle.CenterY = point.Y + _attachedElement.RenderSize.Height / 2;

                rt.Children.Add(newAngle);
                rt.Children.Add(newPosition);
                var mt = new MatrixTransform { Matrix = rt.Value };

                _attachedElement.RenderTransform = mt;
            }
        }

    }
}
