using System.Windows;
using System.Windows.Controls.Primitives;

namespace CustomUI.Behaviors
{
    /// <summary>
    /// Closing parent window when clicked
    /// </summary>
    public class CloseWindowBehavior : BaseWindowBehavior<ButtonBase>
    {
        #region Overrides

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Click += AssociatedObject_Click;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
            base.OnDetaching();
        }

        #endregion

        #region Event Handlers

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.Close();
        }

        #endregion
    }
}
