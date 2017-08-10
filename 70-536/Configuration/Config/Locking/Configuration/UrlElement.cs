using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using AdReportService.Configuration;

namespace Locking.Configuration
{
	public class UrlElement : 
		ConfigurationElement,
		INamedConfigElement
	{
		[ConfigurationProperty("name", DefaultValue="Microsoft", IsKey=true, IsRequired=true)]
		public string Name
		{
			get
			{
				return (string) this["name"];
			}
			set
			{
				this["name"] = value;
			}
		}

		[ConfigurationProperty("url",
			DefaultValue = "http://www.microsoft.com",
			IsRequired = true)]
		[RegexStringValidator(@"\w+:\/\/[\w.]+\S*")]
		public string Url
		{
			get
			{
				return (string)this["url"];
			}
			set
			{
				this["url"] = value;
			}
		}

		[ConfigurationProperty("port", DefaultValue = (int)0, IsRequired = false)]
		[IntegerValidator(MinValue = 0,	MaxValue = 8080, ExcludeRange = false)]
		public int Port
		{
			get
			{
				return (int)this["port"];
			}
			set
			{
				this["port"] = value;
			}
		}

		public string ElementName
		{
			get
			{
				return "urlItem";
			}
		}
	}
}
