using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Northwind.Data.ClassicAdo
{
	public class TerritoryData : BaseDataAccessComponent
	{
		#region Private Constants

		private const string SP_TERRITORIES_BY_REGION_GET = "SP_TERRITORIES_BY_REGION_GET";

		#endregion

		#region Construction

		public TerritoryData(string connectionString)
			: base(connectionString)
		{
		}

		#endregion

		#region Public Methods

		[DataObjectMethod(DataObjectMethodType.Select)]
		public List<Territory> GetTerritoriesByRegion(string regionId)
		{
			List<Territory> retval = new List<Territory>();

			using (DbCommand cmd = GetStoredProcCommand(SP_TERRITORIES_BY_REGION_GET, regionId))
			{
				using (DbDataReader reader = ExecuteReader(cmd))
				{
					while (reader.Read())
					{
						retval.Add(new Territory()
						{
							ID = reader.GetString(0),
							Description = reader.GetString(1)
						});
					}
				}
			}

			return retval;
		}

		#endregion
	}
}
