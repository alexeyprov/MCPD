using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LoggerPack
{
	public abstract class FileLoggerBase : Logger
	{
		#region Private Constants

		private const string APPEND_PARAMETER = "append";
		private const string SHOWSUMMARY_PARAMETER = "showsummary";
		private const string FILENAME_PARAMETER = "logfile";

		private const char PARAM_SEPARATOR = ';';
		private const char NAME_VALUE_SEPARATOR = '=';
		private const char SPACE_SEPARATOR = ' ';
		private const char DQOUTE_SEPARATOR = '\"';

		#endregion

		#region Private Fields

		private bool _append = false;
		private bool _showSummary = false;
		private string _fileName = "loggerpack.log";

		#endregion

		#region Public Properties

		public bool Append
		{
			get
			{
				return _append;
			}
		}

		public bool ShowSummary
		{
			get
			{
				return _showSummary;
			}
		}

		public string FileName
		{
			get
			{
				return _fileName;
			}
		}

		#endregion

		#region Overrides

		public override void Initialize(IEventSource eventSource)
		{
			InitializeParameters();
		}

		#endregion

		#region Implementation

		protected void InitializeParameters()
		{
			if (null == Parameters)
			{
				// nothing to parse; use defaults
			}

			foreach (string param in Parameters.Split(PARAM_SEPARATOR))
			{
				string[] nameValuePair = param.Split(NAME_VALUE_SEPARATOR);

				if (nameValuePair.Length > 1)
				{
					string name = nameValuePair[0].ToLower().Trim();
					switch (name)
					{
						case APPEND_PARAMETER:
							Boolean.TryParse(nameValuePair[1].Trim(), out _append);
							break;
						case SHOWSUMMARY_PARAMETER:
							Boolean.TryParse(nameValuePair[1].Trim(), out _showSummary);
							break;
						case FILENAME_PARAMETER:
							_fileName = nameValuePair[1].Trim(SPACE_SEPARATOR, DQOUTE_SEPARATOR);
							break;
						default:
							break;
					}
				}
			}
		}

		#endregion
	}
}
