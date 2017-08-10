using System;
using System.ComponentModel;
using System.Data.Common;

namespace Northwind.Data.ClassicAdo
{
	public class ProductData : BaseDataAccessComponent
	{
		#region Private Constants

		private const string SP_PRODUCT_STATISTICS_GET = "SP_PRODUCT_STATISTICS_GET";

		#endregion

		#region Construction

		public ProductData(string connectionString)
			: base(connectionString)
		{
		}

		#endregion

		#region Public Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public DbDataReader GetProductStatistics()
		{
			using (DbCommand cmd = GetStoredProcCommand(SP_PRODUCT_STATISTICS_GET))
			{
				return ExecuteReader(cmd);
			}
		}

		#endregion
	}
}
