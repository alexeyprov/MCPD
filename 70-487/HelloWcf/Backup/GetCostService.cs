using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloWcf
{
	// NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in App.config.
	public class GetCostService : IGetCostService
	{
		public string GetTotalCost(ProductData order)
		{
			return String.Format("You've ordered {0} items of {1} with total cost of {2:C}",
				order.ItemCount,
				order.Name,
				order.ItemCount * order.ItemPrice);
		}
	}
}
