using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;

namespace APService
{
	public partial class FirstService : ServiceBase
	{
		public const string LOGNAME = "AP Services Log";
		public const int COMMIT_COMMAND = 200;
		public const int ROLLBACK_COMMAND = 210;

		public FirstService()
		{
			InitializeComponent();
		}

		#region ServiceBase Events
		protected override void OnStart(string[] args)
		{
			CreateEventLog();
			evtLog.WriteEntry("AP First Service is started");
			LogToFile("FirstService is started at {0}", DateTime.Now);

			perfCtrMine.RawValue = 0;

			CreateFile(Files.Transaction);

			if (!File.Exists(GetPath(Files.DB)))
			{
				CreateFile(Files.DB);
			}
		}

		protected override void OnStop()
		{
			evtLog.WriteEntry("AP First Service is stopped");
			LogToFile("FirstService is stopped at {0}", DateTime.Now);

			if (!File.Exists(GetPath(Files.Transaction)))
			{
				File.Delete(GetPath(Files.Transaction));
			}
		}

		protected override void OnCustomCommand(int command)
		{
			base.OnCustomCommand(command);
			string cmdName = "(unknown)";
			switch (command)
			{
				case COMMIT_COMMAND:
					Commit();
					break;
				case ROLLBACK_COMMAND:
					Rollback();
					break;
				default:
					Debug.Fail("Unexpected custom command");
					break;
			}
		}
		#endregion

		#region Implementation
		private enum Files
		{
			Log,
			Transaction,
			DB
		}

		private static string GetPath(Files f)
		{
			string fname;
			switch (f)
			{
				case Files.Log:
					fname = "log.txt";
					break;
				case Files.Transaction:
					fname = "transaction.tmp";
					break;
				case Files.DB:
					fname = "customers.db";
					break;
			}

			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fname);
		}

		private static void CreateFile(Files f)
		{
			using (FileStream fs = new FileStream(GetPath(f), FileMode.Create))
			{
			}
		}

		private static void LogToFile(string msg, params object[] args)
		{
			using (StreamWriter sw = new StreamWriter(GetPath(Files.Log), true))
			{
				sw.WriteLine(msg, args);
			}
		}

		private void Commit()
		{
			using (StreamReader r = new StreamReader(GetPath(Files.Transaction)))
			{
				using (StreamWriter w = new StreamWriter(GetPath(Files.DB), true))
				{
					w.WriteLine(r.ReadToEnd());
				}
			}
			TruncateTransFile();

			evtLog.WriteEntry(String.Format("Customer no. {0} registered",
				perfCtrMine.Increment()));
		}

		private void Rollback()
		{
			TruncateTransFile();
			evtLog.WriteEntry("Transaction is rolled back", EventLogEntryType.Warning);
		}

		private static void TruncateTransFile()
		{
			using (FileStream fs = new FileStream(GetPath(Files.Transaction), FileMode.Truncate))
			{
				fs.Flush();
			}
		}

		private void CreateEventLog()
		{
#if NATIVE_LOG
			if (!System.Diagnostics.EventLog.SourceExists(this.ServiceName))
			{
				System.Diagnostics.EventLog.CreateEventSource(this.ServiceName, LOGNAME);
			}

			evtLog.Source = this.ServiceName;
#endif
		}
		#endregion
	}
}
