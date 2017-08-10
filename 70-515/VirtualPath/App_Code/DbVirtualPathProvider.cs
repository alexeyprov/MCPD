using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

/// <summary>
/// Summary description for MyVirtualPathProvider
/// </summary>
public class DbVirtualPathProvider : VirtualPathProvider
{

	#region Constants

	//private const string EXISTENCE_CHECK_COMMAND = "SELECT COUNT(*) FROM AspContent WHERE [FileName] = @FileName";
	private const string SELECT_COMMAND = "SELECT [Content] FROM AspContent WHERE [FileName] = @FileName";
	private const string FILE_NAME_PARAMETER = "@FileName";

	#endregion

	#region Private Fields

	private DbProviderFactory _dbFactory;
	private string _connectionString;

	#endregion

	#region Constructor

	public DbVirtualPathProvider()
	{
		ConnectionStringSettings connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.CONTENT_DB_CONNECTION_STRING];
		_dbFactory = DbProviderFactories.GetFactory(connString.ProviderName);
		_connectionString = connString.ConnectionString;
	}

	#endregion

	public override bool FileExists(string virtualPath)
	{
		try
		{
			CheckOrCreateVirtualFile(virtualPath, true);
		}
		catch (FileNotFoundException)
		{
			return Previous.FileExists(virtualPath);
		}
		return true;
	}

	public override VirtualFile GetFile(string virtualPath)
	{
		try
		{
			return CheckOrCreateVirtualFile(virtualPath, false);
		}
		catch (FileNotFoundException)
		{
			return Previous.GetFile(virtualPath);
		}
	}

	public static void AppInitialize()
	{
		HostingEnvironment.RegisterVirtualPathProvider(new DbVirtualPathProvider());
	}

	private DbVirtualFile CheckOrCreateVirtualFile(string virtualPath, bool checkOnly)
	{
		const int BUFFER_SIZE = 0x400;

		using (DbConnection connection = CreateConnection())
		{
			using (DbCommand command = CreateCommand(connection, SELECT_COMMAND))
			{
				DbParameter parameter = CreateParameter(FILE_NAME_PARAMETER, DbType.String,
					Path.GetFileName(virtualPath));

				command.Parameters.Add(parameter);
				connection.Open();

				using (DbDataReader reader = command.ExecuteReader())
				{
					if (!reader.Read())
					{
						throw new FileNotFoundException();
					}

					if (checkOnly)
					{
						return null;
					}

					long bytesRead;
					long offset = 0; //DataAccessConfig.BlobOffset;

					MemoryStream stream = new MemoryStream();

					do
					{
						byte[] buffer = new byte[BUFFER_SIZE];
						bytesRead = reader.GetBytes(0, offset, buffer, 0, BUFFER_SIZE);
						buffer = FileEncoding.GetBytes(Encoding.Unicode.GetString(buffer, 0, (int) bytesRead));

						stream.Write(buffer, 0, buffer.Length);

						offset += bytesRead;
					}
					while (BUFFER_SIZE == bytesRead);

					stream.Seek(0, SeekOrigin.Begin);

					return new DbVirtualFile(virtualPath, stream);
				}
			}
		}
	}

	private DbConnection CreateConnection()
	{
		DbConnection connection = _dbFactory.CreateConnection();
		connection.ConnectionString = _connectionString;

		return connection;
	}

	private DbCommand CreateCommand(DbConnection connection, string commandText)
	{
		DbCommand command = _dbFactory.CreateCommand();
		command.CommandType = CommandType.Text;
		command.CommandText = commandText;
		command.Connection = connection;

		return command;
	}

	private DbParameter CreateParameter(string name, DbType type, object value)
	{
		DbParameter parameter = _dbFactory.CreateParameter();
		parameter.ParameterName = name;
		parameter.DbType = type;
		parameter.Value = value;

		return parameter;
	}

	private Encoding FileEncoding
	{
		get
		{
			GlobalizationSection section = (GlobalizationSection)WebConfigurationManager.GetSection("system.web/globalization");
			return section.FileEncoding;
		}
	}
}