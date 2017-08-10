using System.Windows.Input;

namespace HelloWpf.Commands
{
    public static class Core
    {
        public static readonly RoutedUICommand DynamicXaml = new RoutedUICommand(
            "Dynamic XAML",
            "DynamicXaml",
            typeof(Core),
            new InputGestureCollection()
                    {
                        new KeyGesture(Key.D, ModifierKeys.Control)
                    });

        public static readonly RoutedUICommand ElementBinding = new RoutedUICommand(
            "Element Binding",
            "ElementBinding",
            typeof(Core),
            new InputGestureCollection()
                    {
                        new KeyGesture(Key.E, ModifierKeys.Control | ModifierKeys.Shift)
                    });

        public static readonly RoutedUICommand MonitoredCommands = new RoutedUICommand(
            "Monitored Commands",
            "MonitoredCommands",
            typeof(Core));

        public static readonly RoutedUICommand Reverse = new RoutedUICommand(
            "Reverse Last Action",
            "Reverse",
            typeof(UiTricks),
            new InputGestureCollection()
            {
                new KeyGesture(Key.R, ModifierKeys.Control)
            });

        public static readonly RoutedUICommand FlowDocument = new RoutedUICommand(
            "Flow Document",
            "FlowDocument",
            typeof(Core));
    }
}
