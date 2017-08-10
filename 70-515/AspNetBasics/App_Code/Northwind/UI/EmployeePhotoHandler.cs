using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Configuration;

using Northwind.Configuration;

namespace Northwind.UI
{
	/// <summary>
	/// Summary description for EmployeePhotoHandler
	/// </summary>
	public class EmployeePhotoHandler : IHttpAsyncHandler
	{
		#region Constants

		private const string SELECT_PHOTO_SQL = "SELECT PHOTO FROM DBO.EMPLOYEES WHERE EMPLOYEEID = @ID";
		private const string EMPLOYEE_ID_DB_PARAM = "@ID";

		private const int BUFFER_SIZE = 0x1000;
		private const string IMAGE_MIME_TYPE = "image/jpeg";

		private const string EMPLOYEE_ID_PARAM = "ID";

		private const CommandBehavior READER_OPTIONS = CommandBehavior.CloseConnection | CommandBehavior.SingleRow | CommandBehavior.SequentialAccess;

		#endregion

		#region Member Variables

		private HttpContext _context;
		private DbCommand _cmd;

		#endregion

		#region IHttpAsyncHandler Members

		public void ProcessRequest(HttpContext context)
		{
			//TODO: refactor with strategy pattern
			EndRequestHelper(BeginRequestHelper(context, null, null));
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback callback, object state)
		{
			return BeginRequestHelper(context, state, callback);
		}

		void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
		{
			EndRequestHelper(result);
		}

		#endregion

		#region Implementation

		private IAsyncResult BeginRequestHelper(HttpContext context, object state, AsyncCallback callback)
		{
			_context = context;
			_cmd = null;

			_context.Response.ContentType = IMAGE_MIME_TYPE;

			byte[] cachedImage = _context.Cache[CacheKey] as byte[];

			if (cachedImage != null)
			{
				return new CompletedSyncResult<byte[]>(cachedImage, state, callback);
			}

			DataAccessConfigurationSection dataAccessConfig = ((AspNetBasicsApplication)_context.ApplicationInstance).DataAccessConfig;
			DbProviderFactory dbFactory = DbProviderFactories.GetFactory(dataAccessConfig.ProviderFactory);

			DbConnection cn = dbFactory.CreateConnection();
			cn.ConnectionString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ASYNC_CONNECTION_STRING].ConnectionString;

			Exception connectionError = null;
			try
			{
				cn.Open();
			}
			catch (InvalidOperationException iex)
			{
				connectionError = iex;
			}
			catch (DbException dex)
			{
				connectionError = dex;
			}

			if (connectionError != null)
			{
				return new CompletedSyncResult<byte[]>(connectionError, state, callback);
			}

			_cmd = dbFactory.CreateCommand();
			_cmd.Connection = cn;
			_cmd.CommandText = SELECT_PHOTO_SQL;

			DbParameter p = dbFactory.CreateParameter();
			p.ParameterName = EMPLOYEE_ID_DB_PARAM;
			p.Value = EmployeeId;

			_cmd.Parameters.Add(p);

			if (callback != null)
			{
				// asynchronous execution
				return ((SqlCommand)_cmd).BeginExecuteReader(callback, state, READER_OPTIONS);
			}
			else
			{
				// synchronous execution
				try
				{
					return new CompletedSyncResult<IDataReader>(_cmd.ExecuteReader(READER_OPTIONS), state, callback);
				}
				catch (DbException dex)
				{
					return new CompletedSyncResult<byte[]>(dex, state, callback);
				}
			}
		}

		private void EndRequestHelper(IAsyncResult result)
		{
			IDataReader reader = null;
			byte[] image = null;

			try
			{
				CompletedSyncResult<byte[]> cachedResult = result as CompletedSyncResult<byte[]>;
				CompletedSyncResult<IDataReader> readerResult = result as CompletedSyncResult<IDataReader>;

				if (cachedResult != null)
				{
					image = cachedResult.Result;
				}
				else if (readerResult != null)
				{
					// sycnronous execution
					reader = readerResult.Result;
				}
				else
				{
					// asynchronous execution
					reader = ((SqlCommand)_cmd).EndExecuteReader(result);
				}

				if (null == image && reader != null && reader.Read())
				{
					byte[] buffer = new byte[BUFFER_SIZE];
					long bytesRead;
					long offset = DataAccessConfig.BlobOffset;

					using (MemoryStream stream = new MemoryStream())
					{
						do
						{
							bytesRead = reader.GetBytes(0, offset, buffer, 0, BUFFER_SIZE);
							offset += bytesRead;
							//_context.Response.OutputStream.Write(buffer, 0, (int) bytesRead);
							stream.Write(buffer, 0, (int)bytesRead);
						}
						while (BUFFER_SIZE == bytesRead);

						stream.Flush();
						image = stream.GetBuffer();

						_context.Cache.Insert(CacheKey, image
#if MSMQ_CONFIGURED
								, new MessageQueueCacheDependency(ConstantsHelper.APPLICATION_MESSAGE_QUEUE, cacheKey)
#endif
							);
					}
				}
			}
			catch (InvalidOperationException)
			{
				//TODO: log
				RenderError("Error accessing DB");
			}
			catch (SqlException)
			{
				//TODO: log
				RenderError("Error accessing DB");
			}
			finally
			{
				if (_cmd != null)
				{
					_cmd.Dispose();
					_cmd = null;
				}

				if (reader != null)
				{
					reader.Dispose();
				}
			}

			if (image != null)
			{
				_context.Response.BinaryWrite(image);
			}

			_context.Response.Flush();
			_context.Response.Close();
		}

		private void RenderError(string errorMessage)
		{
			// calculate the image width by message length
			using (Bitmap bitmap = new Bitmap(7 * errorMessage.Length, 30))
			{
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					using (Brush redBrush = new SolidBrush(Color.DarkRed), 
						whiteBrush = new SolidBrush(Color.White))
					{
						// create a background filler
						g.FillRectangle(redBrush, 0, 0, bitmap.Width, bitmap.Height);

						// draw our message
						g.DrawString(errorMessage, new Font("Tahoma", 10, FontStyle.Bold),
							whiteBrush, new PointF(5, 5));
					}
				}

				// stream it to the output
				bitmap.Save(_context.Response.OutputStream,
					System.Drawing.Imaging.ImageFormat.Jpeg);
			}
		}

		private DataAccessConfigurationSection DataAccessConfig
		{
			get
			{
				return ((AspNetBasicsApplication)_context.ApplicationInstance).DataAccessConfig;
			}
		}

		private string EmployeeId
		{
			get
			{
				return _context.Request.Params[EMPLOYEE_ID_PARAM];
			}
		}

		private string CacheKey
		{
			get
			{
				return ConstantsHelper.EMPLPOYEE_IMAGE_CACHE_KEY_PREFIX + EmployeeId;
			}
		}

		#endregion
	}
}