using System.Windows;
using System.Windows.Input;

namespace CustomUI.Behaviors
{
    public class MoveWindowBehavior : BaseWindowBehavior<UIElement>
    {
        #region Overrides

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;

            base.OnDetaching();
        }

        #endregion

        #region Event Handlers

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ParentWindow.DragMove();
        }

        #endregion
    }
}
