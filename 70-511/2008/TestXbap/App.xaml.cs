using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace TestXbap
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			System.Threading.Thread.CurrentThread.CurrentUICulture =
				System.Threading.Thread.CurrentThread.CurrentCulture;
		}
	}
}
