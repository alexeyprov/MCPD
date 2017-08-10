using System.Configuration;

namespace Mvc4Async.Service
{
	public static class Util
	{
		public static string GetRootUri()
		{
			// Get the root URI from Web.config
			return Configuration.WidgetServiceUri;
		}

		public static string GetServiceUri(string srv)
		{
			return GetRootUri() + "api/" + srv;
		}
	}

	public static class Configuration
	{
		private static string _uri;

		public static string WidgetServiceUri
		{
			get
			{
				if (!string.IsNullOrEmpty(_uri))
				{
					return _uri;
				}

				_uri = GetKeyVal("WidgetServiceURI");
				return string.IsNullOrEmpty(_uri) ? "http://localhost:7734/" : _uri;
			}
		}

		public static string GetKeyVal(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}