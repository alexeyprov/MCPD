using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace DataTemplateCreation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var manager = new DataTemplateManager();
            manager.RegisterDataTemplate<TextViewModel, TextView>();
            manager.RegisterObsoleteDataTemplate<TextViewModelObsolete, TextView>();
        }
    }
}
