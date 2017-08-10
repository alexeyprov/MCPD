using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NorthwindMVC.Models
{
	public class PagedModel<T> : 
		BaseModel<T>
		where T : class
	{
		internal IEnumerable<T> GetData(int pageNumber, int pageSize, out int totalCount)
		{
			Contract.Requires(pageNumber >= 0);
			Contract.Requires(pageSize > 0);

			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
			Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), s => s != null));
			Contract.Ensures(Contract.ValueAtReturn(out totalCount) >= 0);

			IEnumerable<T> data = base.GetData().ToArray();

			totalCount = data.Count();
			return data.Skip(pageNumber * pageSize).Take(pageSize);
		}
	}
}