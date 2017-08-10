using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace SingletonDocManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public IEnumerable<string> TestData
        {
            get
            {
                return new[] { "Preved", "Medved" };
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BindingExpression binding = BindingOperations.GetBindingExpression(
                TaskBarInfo,
                System.Windows.Shell.TaskbarItemInfo.ThumbnailClipMarginProperty);

            if (binding != null)
            {
                binding.UpdateTarget();
            }
            else
            {
                TaskBarInfo.ThumbnailClipMargin = new Thickness(1, 1, DocumentList.DesiredSize.Width, DocumentList.DesiredSize.Height);
            }
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ErrorException);
        }
    }
}
