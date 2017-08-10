using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Northwind.Data.Entities;

namespace HelloWpf.Northwind
{
    internal sealed class EmployeeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EmployeeTemplate { get; set; }
        public DataTemplate ManagerTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Employee employee = (Employee)item;

            return employee.Subordinates.Any() ?
                ManagerTemplate :
                EmployeeTemplate;
        }
    }
}
