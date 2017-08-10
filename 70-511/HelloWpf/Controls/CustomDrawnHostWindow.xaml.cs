using System.Windows;
using System.Windows.Input;

namespace HelloWpf.Controls
{
    /// <summary>
    /// Interaction logic for CustomDrawnHostWindow.xaml
    /// </summary>
    public partial class CustomDrawnHostWindow : Window
    {
        public CustomDrawnHostWindow()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement rect = (FrameworkElement)sender;

            MessageBox.Show(string.Format("({0},{1})", rect.ActualWidth, rect.ActualHeight));
        }
    }
}
