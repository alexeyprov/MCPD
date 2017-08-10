using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace AdventureWorks.Data.Linq
{
	partial class AdventureWorksDataContext
	{
	}

	[MetadataType(typeof(SalesOrderDetailMetadata))]
	partial class SalesOrderDetail
	{
		public class SalesOrderDetailMetadata
		{
			[Required]
			public object UnitPrice
			{
				get;
				set;
			}

			[Range(1, 100)]
			public object OrderQty
			{
				get;
				set;
			}
		}
	}
}
