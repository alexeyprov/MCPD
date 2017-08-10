using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HelloWpf.UiTricks
{
    /// <summary>
    /// Interaction logic for DragnDropWindow.xaml
    /// </summary>
    public partial class DragnDropWindow : Window
    {
        #region Constructor

        public DragnDropWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = e.Source as TextBox;
            Label label = e.Source as Label;

            string content;
            DependencyObject dragSource;

            if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
            {
                dragSource = textBox;
                content = string.IsNullOrEmpty(textBox.SelectedText) ?
                    textBox.Text :
                    textBox.SelectedText;
            }
            else if (label != null)
            {
                dragSource = label;
                content = label.Content.ToString();
            }
            else
            {
                return;
            }

            DragDrop.DoDragDrop(dragSource, content, DragDropEffects.Copy);
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.Text) ?
                DragDropEffects.Copy :
                DragDropEffects.None;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string data = e.Data.GetData(DataFormats.Text) as string;

            if (!string.IsNullOrEmpty(data))
            {
                ((ContentControl)e.Source).Content = data.Substring(0, Math.Min(data.Length, 100));
            }
        }

        #endregion
    }
}
