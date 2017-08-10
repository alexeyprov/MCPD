using System.Configuration;

namespace HelloWpf.Northwind.Configuration
{
    /// <summary>
    /// Summary description for DataAccessConfigurationSection
    /// </summary>
    public class DataAccessConfigurationSection : ConfigurationSection
	{
		#region Private Constants

		private const string START_PARAMETER_INDEX_PROPERTY = "StartParameterIndex";
		private const string BLOB_OFFSET_INDEX_PROPERTY = "BlobOffset";
		private const string PROVIDER_FACTORY_PROPERTY = "ProviderFactory";

		#endregion

		public DataAccessConfigurationSection()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[ConfigurationProperty(START_PARAMETER_INDEX_PROPERTY, IsRequired = false, DefaultValue = 0)]
		public int StartParameterIndex
		{
			get
			{
				return (int)this[START_PARAMETER_INDEX_PROPERTY];
			}
			set
			{
				this[START_PARAMETER_INDEX_PROPERTY] = value;
			}
		}

		[ConfigurationProperty(BLOB_OFFSET_INDEX_PROPERTY, IsRequired = false, DefaultValue = 0)]
		public int BlobOffset
		{
			get
			{
				return (int)this[BLOB_OFFSET_INDEX_PROPERTY];
			}
			set
			{
				this[BLOB_OFFSET_INDEX_PROPERTY] = value;
			}
		}

		[ConfigurationProperty(PROVIDER_FACTORY_PROPERTY, IsRequired = true)]
		public string ProviderFactory
		{
			get
			{
				return (string)this[PROVIDER_FACTORY_PROPERTY];
			}
			set
			{
				this[PROVIDER_FACTORY_PROPERTY] = value;
			}
		}
	}
}