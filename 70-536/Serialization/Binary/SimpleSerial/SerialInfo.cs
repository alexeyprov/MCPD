using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

internal class SerialInfo
{
	public SerialInfo(IFormatter fmt, string fileName)
	{
		Init(fmt, fileName);
	}

	public SerialInfo(Type fmtType, string fileName)
	{
		InitFromType(fmtType, fileName);
	}

	public IFormatter Formatter
	{
		get
		{
			Debug.Assert(_fmt != null);
			return _fmt;
		}
	}

	public Stream GetSerializationStream(bool read, string path)
	{
		if (null == path || 0 == path.Length)
		{
			path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}
		Debug.Assert(_fileName != null);
		path = Path.Combine(path, _fileName);
		if (read)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read);
		}
		return new FileStream(path, FileMode.Create, FileAccess.Write);

	}

	protected void Init(IFormatter fmt, string fileName)
	{
		if (null == fmt)
		{
			throw new ArgumentNullException("Serialization formatter cannot be null", "fmt");
		}

		if (null == fileName)
		{
			throw new ArgumentNullException("File name cannot be null", "fileName");
		}

		_fmt = fmt;
		_fileName = fileName;
	}

	protected void InitFromType(Type fmtType, string fileName)
	{
		IFormatter fmt = Activator.CreateInstance(fmtType) as IFormatter;
		if (null == fmt)
		{
			throw new ArgumentException("Invalid type of serialization formatter", "fmtType");
		}
	        Init(fmt, fileName);
	}

	private IFormatter _fmt;
	private string _fileName;
}

internal delegate void SerialInfoAction(SerialInfo fmt);
