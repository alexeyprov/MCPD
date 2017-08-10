using System.Windows;

namespace HelloWpf.Controls
{
    /// <summary>
    /// Interaction logic for FlipPanelHostWindow.xaml
    /// </summary>
    public partial class FlipPanelHostWindow : Window
    {
        public FlipPanelHostWindow()
        {
            InitializeComponent();
        }

        private void cmdFlip_Click(object sender, RoutedEventArgs e)
        {
            FlippingPanel.IsFlipped = false;
        }
    }
}
