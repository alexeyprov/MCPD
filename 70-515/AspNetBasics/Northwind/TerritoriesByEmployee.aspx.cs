using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;

using TerraServerService;

public partial class Northwind_TerritoriesByEmployee : Page
{
	#region Constants

	private const string SP_EMPLOYEE_TERRITORIES_GET = "SP_EMPLOYEE_TERRITORIES_GET";
	private const string QUERY_FAILED_ERROR = "The query has failed.";
	private const string SERVICE_FAILED_ERROR = "The query has failed.";
	private const string CONNECTION_FAILED_ERROR = "Cannot establish connection to the database server.";
	private const string DB_OPERATION_TIMEOUT = "Database async operation timed out.";
	private const string WS_OPERATION_TIMEOUT = "Web service async operation timed out.";

	#endregion

	#region Private Fields

	private SqlCommand _command;
	private IDataReader _reader;
	private TerraService _webService;

	#endregion

	#region Page Events

	protected void Page_Load(object sender, EventArgs e)
	{
#if SINGLE_ASYNC_REQUEST
		AddOnPreRenderCompleteAsync(Page_BeginDbAsyncCall, Page_EndDbAsyncCall);
#else
		Page.RegisterAsyncTask(new PageAsyncTask(Page_BeginDbAsyncCall, Page_EndDbAsyncCall, Page_TimeoutDbAsyncCall, null, true));
		Page.RegisterAsyncTask(new PageAsyncTask(Page_BeginWsAsyncCall, Page_EndWsAsyncCall, Page_TimeoutWsAsyncCall, null, true));
#endif
		lblError.Visible = false;
	}

	private IAsyncResult Page_BeginDbAsyncCall(object sender, EventArgs e, AsyncCallback cb, object state)
	{
		SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ASYNC_CONNECTION_STRING].ConnectionString);
		CompletedSyncResult<IDataReader> syncResult = null;
		try
		{
			connection.Open();
		}
		catch (InvalidOperationException ix)
		{
			syncResult = new CompletedSyncResult<IDataReader>(ix, state, cb);
		}
		catch (SqlException ex)
		{
			syncResult = new CompletedSyncResult<IDataReader>(ex, state, cb);
		}

		if (syncResult != null)
		{
			return syncResult;
		}

		_command = new SqlCommand(SP_EMPLOYEE_TERRITORIES_GET, connection);
		_command.CommandType = CommandType.StoredProcedure;
		return _command.BeginExecuteReader(cb, state, CommandBehavior.CloseConnection);
	}

	private IAsyncResult Page_BeginWsAsyncCall(object sender, EventArgs e, AsyncCallback cb, object state)
	{
		const string COUNTRY = "USA";
		const string STATE = "New Jersey";
		const string CITY = "Parsippany";

		Place p = new Place();
		p.Country = COUNTRY;
		p.State = STATE;
		p.City = CITY;

		_webService = new TerraService();
		return _webService.BeginGetPlaceFacts(p, cb, state);
	}

	private void Page_EndDbAsyncCall(IAsyncResult result)
	{
		if (result is CompletedSyncResult<IDataReader>)
		{
			//TODO: Cast and log error
			DisplayError(CONNECTION_FAILED_ERROR);
		}

		if (_command != null)
		{
			try
			{
				_reader = _command.EndExecuteReader(result);
			}
			catch (SqlException)
			{
				DisplayError(QUERY_FAILED_ERROR);
			}

			_command.Dispose();
			_command = null;
		}
	}

	private void Page_EndWsAsyncCall(IAsyncResult result)
	{
		if (_webService != null)
		{
			try
			{
				PlaceFacts facts = _webService.EndGetPlaceFacts(result);
				lblCityFacts.Text = String.Format("Type: {0}; ({1},{2}); population : {3}",
					facts.PlaceTypeId, facts.Center.Lon, facts.Center.Lat, facts.Population);
			}
			catch (SoapException)
			{
				DisplayError(SERVICE_FAILED_ERROR);
			}

			_webService.Dispose();
			_webService = null;
		}
	}

	private void Page_TimeoutDbAsyncCall(IAsyncResult result)
	{
		DisplayError(DB_OPERATION_TIMEOUT);
	}

	private void Page_TimeoutWsAsyncCall(IAsyncResult result)
	{
		DisplayError(WS_OPERATION_TIMEOUT);
	}


	protected void Page_PreRenderComplete(object sender, EventArgs e)
	{
		if (_reader != null)
		{
			gvwTerritories.DataSource = _reader;
			gvwTerritories.DataBind();
			_reader.Close();
			_reader = null;
		}

		if (_command != null)
		{
			_command.Dispose();
			_command = null;
		}

		if (_webService != null)
		{
			_webService.Dispose();
			_webService = null;
		}
	}

	#endregion

	#region Implementation

	private void DisplayError(string text)
	{
		lblError.Text = text;
		lblError.Visible = true;
	}

	#endregion

}
