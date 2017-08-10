using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.ServiceProcess;
using System.Text;

using LoggerService.Facade;
using LoggerService.Configuration;

namespace LoggerService
{
	partial class LoggerSvc : 
		ServiceBase,
		ILogger
	{
		#region Data Members
		ServiceParamsSection _params;
		bool _remotingConfigured;
		#endregion

		public LoggerSvc()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			Logger.RealLogger = this;
			lock (this)
			{
				ConfigureRemotingOnce(LoadConfiguration());
			}
		}

		protected override void OnStop()
		{
			Logger.RealLogger = null;
		}

		private string LoadConfiguration()
		{
			System.Configuration.Configuration cnf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			_params = (ServiceParamsSection) cnf.Sections["ServiceParameters"];
			return cnf.FilePath;
		}

		private void ConfigureRemotingOnce(string configPath)
		{
			if (!_remotingConfigured)
			{
				RemotingConfiguration.Configure(configPath, false);
				_remotingConfigured = true;
			}
		}

		#region ILogger Members

		void ILogger.WriteMessage(string msg)
		{
			lock (this)
			{
				using (StreamWriter sw = new StreamWriter(_params.FileLocation, true))
				{
					if (_params.UseDateTimePrefix)
					{
						sw.Write("{0:G}>>>", DateTime.Now);
					}
					sw.WriteLine(msg);
				}
			}
		}

		#endregion
	}
}
