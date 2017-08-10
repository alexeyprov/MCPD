using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using HelloSilverlight.Server;

namespace HelloSilverlight
{
	public partial class WebServicesPage : Page
	{
		private TestServiceClient _client;

		public WebServicesPage()
		{
			InitializeComponent();
		}

		// Executes when the user navigates to this page.
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			_client = new TestServiceClient();
			string url = ServiceUrl;
			if (!String.IsNullOrEmpty(url))
			{
				_client.Endpoint.Address = new EndpointAddress(url);
			}
			_client.GetServerTimeCompleted += new EventHandler<GetServerTimeCompletedEventArgs>(client_GetServerTimeCompleted);
		}

		void client_GetServerTimeCompleted(object sender, GetServerTimeCompletedEventArgs e)
		{
			try
			{
				TimeLabel.Text = e.Result.ToLongTimeString();
			}
			catch (Exception)
			{
				TimeLabel.Text = "(error)";
			}
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			_client.CloseAsync();
			base.OnNavigatedFrom(e);

		}

		private void GetTimeButton_Click(object sender, RoutedEventArgs e)
		{
			_client.GetServerTimeAsync();
		}

		private string ServiceUrl
		{
			get
			{
				string pageUrl = HtmlPage.Document.DocumentUri.AbsoluteUri;
				int clientPartStart = pageUrl.IndexOf("/HelloSilverlightTestPage");
				return clientPartStart >= 0 ? pageUrl.Substring(0, clientPartStart) + "/TestService.svc" : String.Empty;
			}
		}

	}
}
