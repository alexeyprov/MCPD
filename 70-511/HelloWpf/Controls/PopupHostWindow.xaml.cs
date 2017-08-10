using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace HelloWpf.Controls
{
    /// <summary>
    /// Interaction logic for PopupHostWindow.xaml
    /// </summary>
    public partial class PopupHostWindow : Window
    {
        public PopupHostWindow()
        {
            InitializeComponent();
        }

        private void run_MouseEnter(object sender, MouseEventArgs e)
        {
            popLink.IsOpen = true;
        }

        private void lnk_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(((Hyperlink)sender).NavigateUri.ToString());
        }
    }
}
