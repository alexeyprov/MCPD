using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web.UI.WebControls;

namespace ControlExtensions
{
	internal static class ExposedSR
	{
		private static readonly ResourceManager _resMgr = new ResourceManager("System.Web", typeof(ObjectDataSourceView).Assembly);

		internal static string GetString(string name)
		{
			return _resMgr.GetString(name, null);
		}

		internal static string GetString(string name, params object[] args)
		{
			string text = _resMgr.GetString(name, null);

			if (args == null || args.Length == 0)
			{
				return text;
			}

			// clip all the string arguments to less than 1K length
			for (int index = 0; index < args.Length; index++)
			{
				string argString = args[index] as string;

				if (argString != null && argString.Length > 0x400)
				{
					args[index] = argString.Substring(0, 0x3fd) + "...";
				}
			}

			return string.Format(CultureInfo.CurrentCulture, text, args);
		}

		internal static readonly string Pessimistic = "ObjectDataSourceView_Pessimistic";
		internal static readonly string InsertNotSupported = "ObjectDataSourceView_InsertNotSupported";
		internal static readonly string UpdateNotSupported = "ObjectDataSourceView_UpdateNotSupported";
		internal static readonly string DeleteNotSupported = "ObjectDataSourceView_DeleteNotSupported";
		internal static readonly string InsertRequiresValues = "ObjectDataSourceView_InsertRequiresValues";
		internal static readonly string Update = "DataSourceView_update";
		internal static readonly string Delete = "DataSourceView_delete";
		internal static readonly string InvalidViewName = "DataSource_InvalidViewName";
		internal static readonly string DataObjectPropertyNotFound = "ObjectDataSourceView_DataObjectPropertyNotFound";
		internal static readonly string DataObjectPropertyReadOnly = "ObjectDataSourceView_DataObjectPropertyReadOnly";
	}
}
