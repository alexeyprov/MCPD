using System.Windows;
using System.Windows.Input;

namespace HelloWpf.UiTricks
{
    /// <summary>
    /// Interaction logic for CalloutWindow.xaml
    /// </summary>
    public partial class CalloutWindow : Window
    {
        public CalloutWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
