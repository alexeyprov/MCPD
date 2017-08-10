using System.Windows.Input;

namespace HelloWpf.Commands
{
    public static class Controls
    {
        public static readonly RoutedUICommand Popup = new RoutedUICommand(
            "Popup",
            "Popup",
            typeof(Controls),
            new InputGestureCollection
            {
                new KeyGesture(Key.F2)
            });

        public static readonly RoutedUICommand ControlTemplateBrowser = new RoutedUICommand(
            "Template Browser",
            "ControlTemplateBrowser",
            typeof(Controls),
            new InputGestureCollection
            {
                new KeyGesture(Key.T, ModifierKeys.Control)
            });

        public static readonly RoutedUICommand ColorPicker = new RoutedUICommand(
            "Color Picker",
            "ColorPicker",
            typeof(Controls));

        public static readonly RoutedUICommand FlipPanel = new RoutedUICommand(
            "Flip Panel",
            "FlipPanel",
            typeof(Controls));

        public static readonly RoutedUICommand CustomDrawn = new RoutedUICommand(
            "Custom Drawn Control",
            "CustomDrawn",
            typeof(Controls));
    }
}
