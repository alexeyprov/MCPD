using System;
using System.Collections.Generic;
using System.IO;

public class FileLogger : IDisposable
{
	#region Private Fields

	private StreamWriter _writer;

	#endregion

	#region Construction/Destruction

	public FileLogger(string fileName)
	{
		_writer = new StreamWriter(fileName);
	}

	public void Dispose()
	{
		if (_writer != null)
		{
			_writer.Dispose();
			_writer = null;
		}
	}

	#endregion

	#region Public Methods

	public void Write(string s)
	{
		CheckStateAndWritePrefix();
		_writer.WriteLine(s);
		_writer.Flush();
	}

	public void Write(Exception ex)
	{
		CheckStateAndWritePrefix();
		_writer.WriteLine(ex.StackTrace);
		_writer.Flush();
	}

	#endregion

    #region Implementation

	private void CheckStateAndWritePrefix()
	{
		if (null == _writer)
		{
			throw new ObjectDisposedException("FileLogger");
		}

		_writer.Write("[{0:G}] ", DateTime.Now);
	}

	#endregion
}
