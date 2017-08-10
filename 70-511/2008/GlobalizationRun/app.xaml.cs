using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace RunDialog
{
    /// <summary>
    /// Interaction logic for app.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args.Length > 0)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(e.Args[0]);
            }
        }
    }
}