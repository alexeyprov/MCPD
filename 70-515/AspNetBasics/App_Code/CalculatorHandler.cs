using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CalculatorHandler
/// </summary>
public class CalculatorHandler : IHttpHandler
{
	#region IHttpHandler Members

	public bool IsReusable
	{
		get
		{
			return true;
		}
	}

	public void ProcessRequest(HttpContext context)
	{
		HttpRequest request = context.Request;
		HttpResponse response = context.Response;

		response.ContentType = "text/plain";

		float op1, op2;

		if (Single.TryParse(request["op1"], out op1) && Single.TryParse(request["op2"], out op2))
		{
			response.Write(String.Format("{0},{1:f}", op1 + op2, DateTime.Now));
		}
		else
		{
			response.Write('-');
		}

		//response.Close();
	}

	#endregion
}