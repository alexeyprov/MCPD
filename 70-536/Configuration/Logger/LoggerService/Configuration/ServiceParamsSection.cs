using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LoggerService.Configuration
{
	class ServiceParamsSection : ConfigurationSection
	{
		static ConfigurationPropertyCollection _props;
		static ConfigurationProperty _locationProp;
		static ConfigurationProperty _dateTimePrefixProp;

		const string PROP_LOCATION = "location";
		const string PROP_DT_PREFIX = "dateTimePrefix";

		static ServiceParamsSection()
		{
			const int MIN_PATH = 1;
			const int MAX_PATH = 260;
			const string DEF_LOG_PATH = "log.txt";
			
			StringValidator pathValidator = new StringValidator(MIN_PATH, MAX_PATH,
				new string(Path.GetInvalidPathChars()));
			_locationProp = new ConfigurationProperty(PROP_LOCATION, typeof(string), DEF_LOG_PATH,
				TypeDescriptor.GetConverter(typeof(string)), pathValidator,
				ConfigurationPropertyOptions.IsRequired);

			_dateTimePrefixProp = new ConfigurationProperty(PROP_DT_PREFIX, typeof(bool), true);

			_props = new ConfigurationPropertyCollection();
			_props.Add(_locationProp);
			_props.Add(_dateTimePrefixProp);
		}

		//[ConfigurationProperty("location", DefaultValue="log.txt", IsRequired=true)]
		//[StringValidator(InvalidCharacters=System.IO.Path.InvalidPathChars.ToString(), MinLength=1, MaxLength=255)]
		public string FileLocation
		{
			get
			{
				string f = (string)base[PROP_LOCATION];
				if (!Path.IsPathRooted(f))
				{
					string rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
					f = Path.Combine(rootFolder, f);
				}
				return f;
			}
			set
			{
				base[PROP_LOCATION] = value;
			}
		}

		//[ConfigurationProperty("dateTimePrefix", DefaultValue=true)]
		public bool UseDateTimePrefix
		{
			get
			{
				return (bool)base[PROP_DT_PREFIX];
			}
			set
			{
				base[PROP_DT_PREFIX] = value;
			}
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return _props;
			}
		}
	}
}
