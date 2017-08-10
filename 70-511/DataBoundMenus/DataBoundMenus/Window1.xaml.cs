using System;
using System.Windows;
using UpdateControls.XAML;

namespace DataBoundMenus
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            DataContext = ForView.Wrap(new ApplicationViewModel(new ApplicationDataModel()));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
