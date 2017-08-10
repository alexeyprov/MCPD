using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

using Northwind;
using Northwind.Configuration;
using Northwind.Data.ClassicAdo;

/// <summary>
/// Summary description for Global
/// </summary>
public class AspNetBasicsApplication : HttpApplication
{
	#region Private Constants

	private const string DATA_ACCESS_CONFIGURATION_SECTION = "DataAccessConfiguration";

	#endregion

	#region Private Members

	private Lazy<DataAccessConfigurationSection> _dataAccessConfig = new Lazy<DataAccessConfigurationSection>(
		() => (DataAccessConfigurationSection) WebConfigurationManager.GetSection(DATA_ACCESS_CONFIGURATION_SECTION)); 

	#endregion

	#region Constructor

	public AspNetBasicsApplication()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	#endregion

	#region Properties

	public DataAccessConfigurationSection DataAccessConfig
	{
		get
		{
			return _dataAccessConfig.Value;
		}
	}

	#endregion

	#region Application Event Handlers

	void Application_Start(object sender, EventArgs e)
	{
		// Code that runs on application startup
		SqlDependency.Start(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);
		BaseDataAccessComponent.Initialize(DataAccessConfig.ProviderFactory, DataAccessConfig.StartParameterIndex);
	}

	void Application_End(object sender, EventArgs e)
	{
		//  Code that runs on application shutdown
		SqlDependency.Stop(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);
	}

	void Application_Error(object sender, EventArgs e)
	{
		Response.Write("<b>Oops! It looks like an error has occured</b><hr/>");
		Response.Write(Server.GetLastError().Message);
		Response.Write("<hr/>");
		Response.Write(Server.GetLastError().ToString());
		Server.ClearError();
	}

	void Session_Start(object sender, EventArgs e)
	{
		// Code that runs when a new session is started
	}

	void Application_PostRequestHandlerExecute(object sender, EventArgs e)
	{
		//Response.Write("<hr/>");
		//Response.Write(String.Format("Session start date: {0}", Session[SessionSpyModule.SESSION_START_KEY]));
	}

	void Session_End(object sender, EventArgs e)
	{
		// Code that runs when a session ends. 
		// Note: The Session_End event is raised only when the sessionstate mode
		// is set to InProc in the Web.config file. If session mode is set to StateServer 
		// or SQLServer, the event is not raised.
	}

	#endregion

}
