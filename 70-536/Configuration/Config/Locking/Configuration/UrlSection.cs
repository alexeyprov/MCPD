using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using AdReportService.Configuration;

namespace Locking.Configuration
{
	using UrlCollection = TypedConfigCollection<UrlElement>;

	public class UrlSection : ConfigurationSection
	{
		[ConfigurationProperty("simple")]
		public UrlElement Simple
		{
			get
			{
				return (UrlElement)this["simple"];
			}
			set
			{
				this["simple"] = value;
			}
		}

		[ConfigurationProperty("urls", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(UrlCollection))]
		public UrlCollection Urls
		{
			get
			{
				return (UrlCollection)this["urls"];
			}
		}

		[ConfigurationProperty("name", DefaultValue = "MyFavorites", IsRequired = true, IsKey = false)]
		[StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
		public string Name
		{
			get
			{
				return (string)this["name"];
			}
			set
			{
				this["name"] = value;
			}
		}
	}
}
