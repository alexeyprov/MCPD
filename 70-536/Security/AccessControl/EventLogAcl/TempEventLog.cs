using System;
using System.Diagnostics;

class TempEventLog : IDisposable
{
	// Data Members
	private EventLog _log;

	// Construction/Destruction
	public TempEventLog(string logName)
	{
		_log = new EventLog(logName);
		_log.Source = logName;
	}

	~TempEventLog()
	{
		Dispose(false);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		string logName = null;
		if (_log != null)
		{
			logName = _log.Log;
		}

		if (disposing)
		{
			if (_log != null)
			{
				_log.Dispose();
				_log = null;
			}
		}

		if (logName != null)
		{
			EventLog.Delete(logName);
		}
	}

	// Properties
	public string Name
	{
		get
		{
			return _log.Log;
		}
	}

	// Operations
	public void WriteInfo(string info)
	{
		_log.WriteEntry(info, EventLogEntryType.Information);
	}
}