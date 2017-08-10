using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Resources;

namespace HelloWpf.Core
{
    /// <summary>
    /// Interaction logic for ExternalXamlWindow.xaml
    /// </summary>
    public partial class ExternalXamlWindow : Window
    {
        private const string RESOURCE_FILE = "Core/ExternalXamlWindow.xaml";

        private Button cmdClickMe;

        public ExternalXamlWindow()
        {
            Width = Height = 285;
            Top = Left = 100;
            Title = "Dynamic XAML Window";

            StreamResourceInfo streamInfo = Application.GetRemoteStream(new Uri(RESOURCE_FILE, UriKind.Relative));
            using (Stream xaml = streamInfo.Stream)
            {
                Content = XamlReader.Load(xaml);
            }

            cmdClickMe = (Button)LogicalTreeHelper.FindLogicalNode(this, "cmdClickMe");
            cmdClickMe.Click += cmdClickMe_Click;
        }

        private void cmdClickMe_Click(object sender, RoutedEventArgs e)
        {
            cmdClickMe.Content = "Thank you!";
        }
    }
}
