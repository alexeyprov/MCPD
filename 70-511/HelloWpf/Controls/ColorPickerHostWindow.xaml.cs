using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace HelloWpf.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickerHostWindow.xaml
    /// </summary>
    public partial class ColorPickerHostWindow : Window
    {
        public ColorPickerHostWindow()
        {
            InitializeComponent();
        }

        private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Debug.WriteLine("Color changed from {0} to {1}", e.OldValue, e.NewValue);
        }
    }
}
