using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace HelloSilverlight
{
	public partial class HomePage : Page
	{
		public HomePage()
		{
			InitializeComponent();
		}

		// Executes when the user navigates to this page.
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private void cmdClickMe_Click(object sender, RoutedEventArgs e)
		{
			txtHello.Text = ("Hello, world!" == txtHello.Text) ? "Goodbye, world!" : "Hello, world!";
		}
	}
}
