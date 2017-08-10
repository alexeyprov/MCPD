using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Summary description for SessionSpyModule
/// </summary>
public class SessionSpyModule : IHttpModule
{
	public const string SESSION_START_KEY = "SESSION_START";
	private const string SESSION_MODULE_NAME = "Session";
	private SessionStateModule _sessionModule;

	#region IHttpModule Members

	public void Dispose()
	{
		if (_sessionModule != null)
		{
			_sessionModule.Start -= new EventHandler(sessionModule_Start);
			_sessionModule = null;
		}
	}

	public void Init(HttpApplication context)
	{
		_sessionModule = (SessionStateModule) context.Modules[SESSION_MODULE_NAME];
		if (_sessionModule != null)
		{
			_sessionModule.Start += new EventHandler(sessionModule_Start);
		}
	}

	void sessionModule_Start(object sender, EventArgs e)
	{
		HttpContext.Current.Session[SESSION_START_KEY] = DateTime.Now;
	}

	#endregion
}
