using System;
using System.Windows;
using System.Windows.Controls;

namespace CustomUI.Interop
{
    /// <summary>
    /// Interaction logic for ButtonWrapper.xaml
    /// </summary>
    public partial class ButtonWrapper : UserControl
    {
        public ButtonWrapper()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event EventHandler Clicked;

        public string Text
        {
            get;
            set;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Clicked != null)
            {
                Clicked(this, EventArgs.Empty);
            }
        }
    }
}
