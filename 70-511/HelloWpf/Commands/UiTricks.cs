using System.Windows.Input;

namespace HelloWpf.Commands
{
    public static class UiTricks
    {
        public static readonly RoutedUICommand CustomBehavior = new RoutedUICommand(
            "Behaviors",
            "CustomBehavior",
            typeof(UiTricks));

        public static readonly RoutedUICommand Reflection = new RoutedUICommand(
            "Reflection",
            "Reflection",
            typeof(UiTricks));

        public static readonly RoutedUICommand SquaresGame = new RoutedUICommand(
            "Squares Game",
            "SquaresGame",
            typeof(UiTricks));

        public static readonly RoutedUICommand Animations = new RoutedUICommand(
            "Animations",
            "Animations",
            typeof(UiTricks));

        public static readonly RoutedUICommand BombGame = new RoutedUICommand(
            "Bomb Drop Grame",
            "BombGame",
            typeof(UiTricks));

        public static readonly RoutedUICommand CalloutWindow = new RoutedUICommand(
            "Callout Window",
            "CalloutWindow",
            typeof(UiTricks));

        public static readonly RoutedUICommand ThreeDDemo = new RoutedUICommand(
            "3D Demo",
            "3D_Demo",
            typeof(UiTricks));

        public static readonly RoutedUICommand WinFormsInterop = new RoutedUICommand(
            "WinForms Interaction",
            "WinForms",
            typeof(UiTricks));

        public static readonly RoutedUICommand DragnDrop = new RoutedUICommand(
            "Drag'n'Drop",
            "DragAndDrop",
            typeof(UiTricks));
    }
}
