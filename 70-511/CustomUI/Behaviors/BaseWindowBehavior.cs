using System;
using System.Windows;
using System.Windows.Interactivity;

namespace CustomUI.Behaviors
{
    /// <summary>
    /// Base behavior for elements inside a window.
    /// </summary>
    /// <typeparam name="T">Element type</typeparam>
    public abstract class BaseWindowBehavior<T> : 
        Behavior<T>
        where T : DependencyObject
    {
        #region Protected Interface

        protected Window ParentWindow
        {
            get;
            private set;
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            DependencyObject dependencyObject = AssociatedObject as DependencyObject;

            if (dependencyObject != null)
            {
                ParentWindow = TreeHelper.FindAncestor(dependencyObject, d => d is Window) as Window;
            }

            if (ParentWindow == null)
            {
                throw new InvalidOperationException(
                    "The behavior can only be used with elements inside a window.");
            }
        }

        #endregion
    }
}
