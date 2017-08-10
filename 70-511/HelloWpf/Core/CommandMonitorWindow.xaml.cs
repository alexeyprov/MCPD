using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using HelloWpf.Commands;

namespace HelloWpf.Core
{
    /// <summary>
    /// Interaction logic for CommandMonitorWindow.xaml
    /// </summary>
    public partial class CommandMonitorWindow : Window
    {
        public CommandMonitorWindow()
        {
            InitializeComponent();
            AddHandler(CommandManager.PreviewExecutedEvent, new ExecutedRoutedEventHandler(OnPreviewExecuted));
        }

        private void OnPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Source is ICommandSource)
            {
                return;
            }

            if (e.Command == Commands.Core.Reverse)
            {
                return;
            }

            TextBox txt = e.Source as TextBox;
            if (txt != null)
            {
                CommandHistoryItem item = new CommandHistoryItem
                {
                    CommandTarget = txt,
                    AffectedProperty = "Text",
                    CommandName = ((RoutedCommand)e.Command).Name,
                    PreviousValue = txt.Text
                };

                ListBoxCommandHistory.Items.Add(item);
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            RemoveHandler(CommandManager.PreviewExecutedEvent, new ExecutedRoutedEventHandler(OnPreviewExecuted));
        }

        private void Reverse_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CommandHistoryItem historyItem = LastHistoryItem;
            e.CanExecute = historyItem != null && historyItem.CanUndo;
        }

        private void Reverse_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CommandHistoryItem historyItem = LastHistoryItem;
            historyItem.Undo();
            ListBoxCommandHistory.Items.Remove(historyItem);
        }

        private CommandHistoryItem LastHistoryItem
        {
            get
            {
                if (ListBoxCommandHistory == null)
                {
                    return null;
                }

                int count = ListBoxCommandHistory.Items.Count;
                return count > 0 ?
                    ListBoxCommandHistory.Items[count - 1] as CommandHistoryItem :
                    null;
            }
        }
    }
}
