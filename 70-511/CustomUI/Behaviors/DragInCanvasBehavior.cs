using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace CustomUI.Behaviors
{
    /// <summary>
    /// Dragging an element inside a <see cref="Canvas"/>.
    /// </summary>
    public class DragInCanvasBehavior : Behavior<UIElement>
    {
        #region Private Fields

        private Canvas _canvas;
        private Point _mouseOffset; 
        
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
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }

        #endregion

        #region Event Handlers

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _canvas = (Canvas)VisualTreeHelper.GetParent(AssociatedObject);
            _mouseOffset = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (_canvas != null)
            {
                Vector location = e.GetPosition(_canvas) - _mouseOffset;

                AssociatedObject.SetValue(Canvas.TopProperty, location.Y);
                AssociatedObject.SetValue(Canvas.LeftProperty, location.X);
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_canvas != null)
            {
                _canvas = null;
                AssociatedObject.ReleaseMouseCapture();
            }
        }

        #endregion
    }
}
