using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureWorks.Data.Linq
{
	public class OrderDetailsComponent
	{
		public IEnumerable<SalesOrderDetail> GetOrderDetails()
		{
			using (AdventureWorksDataContext context = new AdventureWorksDataContext())
			{
				return context.SalesOrderDetails.ToList();
			}
		}

		public void UpdateOrderDetail(SalesOrderDetail detail)
		{
			using (AdventureWorksDataContext context = new AdventureWorksDataContext())
			{
				context.SalesOrderDetails.Attach(detail, true);
				context.SubmitChanges();
			}
		}
	}
}
