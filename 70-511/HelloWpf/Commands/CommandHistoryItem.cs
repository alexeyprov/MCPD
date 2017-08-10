using System;
using System.Reflection;
using System.Windows;

namespace HelloWpf.Commands
{
    internal sealed class CommandHistoryItem
    {
        public string CommandName
        {
            get;
            set;
        }

        public FrameworkElement CommandTarget
        {
            get;
            set;
        }

        public string AffectedProperty
        {
            get;
            set;
        }

        public object PreviousValue
        {
            get;
            set;
        }

        public bool CanUndo
        {
            get
            {
                return CommandTarget != null && !string.IsNullOrEmpty(AffectedProperty);
            }
        }

        public void Undo()
        {
            Type targetType = CommandTarget.GetType();
            PropertyInfo property = targetType.GetProperty(AffectedProperty);
            property.SetValue(CommandTarget, PreviousValue);
        }
    }
}
