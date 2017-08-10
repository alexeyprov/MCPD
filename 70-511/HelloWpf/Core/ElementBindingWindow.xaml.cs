using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HelloWpf.Core
{
    /// <summary>
    /// Interaction logic for ElementBindingWindow.xaml
    /// </summary>
    public partial class ElementBindingWindow : Window
    {
        public ElementBindingWindow()
        {
            InitializeComponent();
        }

        private void SmallSizeButton_Click(object sender, RoutedEventArgs e)
        {
            FontSlider.Value = 15;
        }

        private void LargeSizeButton_Click(object sender, RoutedEventArgs e)
        {
            FontSlider.Value = 35;
        }

        private void MediumSizeButton_Click(object sender, RoutedEventArgs e)
        {
            SampleTextBlock.FontSize = 25;
        }

        private void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Control)sender).Background = Brushes.Yellow;
        }

        private void Element_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Control)sender).Background = null;
        }
    }
}
