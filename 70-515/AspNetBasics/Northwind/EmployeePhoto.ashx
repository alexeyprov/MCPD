<%@ WebHandler Language="C#" Class="EmployeePhoto" %>

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Caching;

public class EmployeePhoto : IHttpHandler
{
	#region Constants

	private const string SELECT_PHOTO_SQL = "SELECT PHOTO FROM DBO.EMPLOYEES WHERE EMPLOYEEID = @ID";
	private const string EMPLOYEE_ID_DB_PARAM = "@ID";
	
	private const int BUFFER_SIZE = 0x100;
	private const int SQL_SERVER_BLOB_OFFSET = 78;
	private const string IMAGE_MIME_TYPE = "image/jpeg";
	
	private const string EMPLOYEE_ID_PARAM = "ID";
	
	#endregion

	#region IHttpHandler Members
	
	public void ProcessRequest (HttpContext context) 
	{
		context.Response.ContentType = IMAGE_MIME_TYPE;
		string id = context.Request.Params[EMPLOYEE_ID_PARAM];

		string cacheKey = ConstantsHelper.EMPLPOYEE_IMAGE_CACHE_KEY_PREFIX + id;
		byte[] cachedImage = context.Cache[cacheKey] as byte[];

		if (null == cachedImage)
		{
			using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString))
			{
				cn.Open();
				
				using (MemoryStream stream = new MemoryStream())
				{
					using (SqlCommand cmd = new SqlCommand(SELECT_PHOTO_SQL, cn))
					{
						SqlParameter p = new SqlParameter(EMPLOYEE_ID_DB_PARAM, id);
						cmd.Parameters.Add(p);
						using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
						{
							if (reader.Read())
							{
								byte[] buffer = new byte[BUFFER_SIZE];
								long bytesRead;
								long offset = SQL_SERVER_BLOB_OFFSET;

								do
								{
									bytesRead = reader.GetBytes(0, offset, buffer, 0, BUFFER_SIZE);
									offset += bytesRead;
									//context.Response.OutputStream.Write(buffer, 0, (int) bytesRead);
									stream.Write(buffer, 0, (int) bytesRead);
								}
								while (BUFFER_SIZE == bytesRead);
							}
						}

						stream.Flush();
						cachedImage = stream.GetBuffer();
						context.Cache.Insert(cacheKey, cachedImage, 
							new MessageQueueCacheDependency(ConstantsHelper.APPLICATION_MESSAGE_QUEUE, cacheKey));
					}
				}
			}
		}

		context.Response.BinaryWrite(cachedImage);
		
		context.Response.Flush();
		context.Response.Close();		
	}

	public bool IsReusable
	{
		get
		{
			return false;
		}
	}

	#endregion
	
}