using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SingletonDocManager
{
    /// <summary>
    /// Interaction logic for DocumentWindow.xaml
    /// </summary>
    public partial class DocumentWindow : Window
    {
        public DocumentWindow(string documentPath)
        {
            InitializeComponent();
            LoadDocument(documentPath);
        }

        private void LoadDocument(string documentPath)
        {
            if (File.Exists(documentPath))
            {
                Title = documentPath;
                Content = File.ReadAllText(documentPath);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((App)Application.Current).UnloadDocument(Title);
        }
    }
}
