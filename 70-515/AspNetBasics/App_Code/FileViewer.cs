using System;
using System.IO;
using System.Web;

public class FileViewer : IHttpHandler
{
	private const string FILE_NAME_PARAMETER = "file";


	public void ProcessRequest(HttpContext context)
	{
		//context.Response.ContentType = "text/plain";
		context.Response.Write("<html>");
		try
		{
			using (TextReader reader = OpenReader(context))
			{
				WriteFormatted(context, "<h1>File contents of {0}</h1>", 
					context.Request[FILE_NAME_PARAMETER]); 

				string s = reader.ReadLine();
				while (s != null)
				{
					WriteFormatted(context, "{0}<br>", s);
					s = reader.ReadLine();
				}
			}
		}
		catch (ArgumentException)
		{
			WriteUsageInfo(context.Response);
		}
		context.Response.Write("</html>");
		context.Response.Flush();
		context.Response.Close();
	}

	private TextReader OpenReader(HttpContext context)
	{
		try
		{
			string fileName = context.Request["file"];
			fileName = context.Server.MapPath(fileName);
			return new StreamReader(fileName);
		}
		catch (Exception e)
		{
			throw new ArgumentException("File not found", e);
		}
	}

	private void WriteUsageInfo(HttpResponse response)
	{
		response.Write("Usage:<br/>");
		response.Write("FileViewer.ashx?file=<i>&lt;file_name&gt;</i>");
	}

	private void WriteFormatted(HttpContext context, string format, params string[] args)
	{
		string[] formattedArgs = Array.ConvertAll(args, s => context.Server.HtmlEncode(s));
		string result = String.Format(format, formattedArgs);
		context.Response.Write(result.Replace(" ", "&nbsp;").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"));
		context.Response.Write(Environment.NewLine);
	}

	public bool IsReusable
	{
		get
		{
			return false;
		}
	}

}
