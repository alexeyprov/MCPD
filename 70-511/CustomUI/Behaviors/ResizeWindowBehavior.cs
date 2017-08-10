using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace CustomUI.Behaviors
{
    /// <summary>
    /// Resizes the parent window by handling mouse events
    /// </summary>
    public class ResizeWindowBehavior : BaseWindowBehavior<Rectangle>
    {
        #region Private Fields

        private Point? _startPoint; 

        #endregion

        #region Public Properties

        public bool IsHorizontal
        {
            get;
            set;
        }

        public bool IsVertical
        {
            get;
            set;
        }

        #endregion

        #region Overrides

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;

            base.OnDetaching();
        }

        #endregion

        #region Event Handlers

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = GetScreenPoint(e);
            AssociatedObject.CaptureMouse();
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (_startPoint.HasValue)
            {
                ResizeWindow(e);
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_startPoint.HasValue)
            {
                ResizeWindow(e);

                _startPoint = null;
                AssociatedObject.ReleaseMouseCapture();
            }
        }

        #endregion

        #region Implementation

        private Point GetScreenPoint(MouseEventArgs e)
        {
            return ParentWindow.PointToScreen(e.GetPosition(ParentWindow));
        }

        private void ResizeWindow(MouseEventArgs e)
        {
            Point currentPoint = GetScreenPoint(e);
            Vector delta = currentPoint - _startPoint.Value;

            if (IsHorizontal && delta.X != 0.0)
            {
                ParentWindow.Width += delta.X;
            }

            if (IsVertical && delta.Y != 0.0)
            {
                ParentWindow.Height += delta.Y;
            }

            _startPoint = currentPoint;
        }

        #endregion
    }
}
