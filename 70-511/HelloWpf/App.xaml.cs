using System;
using System.Configuration;
using System.Windows;
using System.Windows.Forms.Integration;
using HelloWpf.Northwind.Configuration;
using Northwind.Data.ClassicAdo;

namespace HelloWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private Constants

        private const string DATA_ACCESS_CONFIGURATION_SECTION = "DataAccessConfiguration";

        #endregion

        #region Private Members

        private Lazy<DataAccessConfigurationSection> _dataAccessConfig = new Lazy<DataAccessConfigurationSection>(
            () => (DataAccessConfigurationSection)ConfigurationManager.GetSection(DATA_ACCESS_CONFIGURATION_SECTION));

        #endregion

        #region Public Properties

        public DataAccessConfigurationSection DataAccessConfig
        {
            get
            {
                return _dataAccessConfig.Value;
            }
        }

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Code that runs on application startup
            BaseDataAccessComponent.Initialize(DataAccessConfig.ProviderFactory, DataAccessConfig.StartParameterIndex);

            WindowsFormsHost.EnableWindowsFormsInterop();
            System.Windows.Forms.Application.EnableVisualStyles();
        }
    }
}
