using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

/// <summary>
/// Summary description for DbVirtualFile
/// </summary>
public class DbVirtualFile : VirtualFile
{
	private Stream _fileStream;

	public DbVirtualFile(string path, Stream fileStream) : 
		base(path)
	{
		_fileStream = fileStream;
	}

	public override Stream Open()
	{
		return _fileStream;
	}
}