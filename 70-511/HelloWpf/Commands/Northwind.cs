using System.Windows.Input;

namespace HelloWpf.Commands
{
    public static class Northwind
    {
        public static readonly RoutedUICommand Customers =
            new RoutedUICommand("Customers", "Customers", typeof(Northwind));

        public static readonly RoutedUICommand Employees =
            new RoutedUICommand("Employees", "Employees", typeof(Northwind));

        public static readonly RoutedUICommand Suppliers =
            new RoutedUICommand("Suppliers", "Suppliers", typeof(Northwind));

        public static readonly RoutedUICommand Geography =
            new RoutedUICommand("Geography", "Geography", typeof(Northwind));

        public static readonly RoutedUICommand Products =
            new RoutedUICommand("Products", "Products", typeof(Northwind));
    }
}
