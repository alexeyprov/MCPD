using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.Linq;

namespace Northwind.UI.Linq
{
	public partial class ProductsByCategoryPage : Northwind.UI.NorthwindBasePage
	{
		#region Private Constants

		private const string CATEGORY_ID_COLUMN = "CategoryID";

		#endregion

		#region Private Fields

		NorthwindDataContext _northwindDataContext;

		#endregion

		#region Event Handlers

		protected void Page_Load(object sender, EventArgs e)
		{
			_northwindDataContext = new NorthwindDataContext(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);

			var categories = from category in _northwindDataContext.Categories
							 join product in _northwindDataContext.Products on category.CategoryID equals product.CategoryID into gj
							 where gj.Max(p => p.UnitPrice) > 50
							 select new
							 {
								 CategoryID = category.CategoryID,
								 Name = category.CategoryName,
								 ProductCount = gj.Count(),
								 Description = category.Description
							 };

			BindToSubset(grdCategories, categories);
		}

		protected void Page_PreRenderComplete(object sender, EventArgs e)
		{
			if (_northwindDataContext != null)
			{
				_northwindDataContext.Dispose();
				_northwindDataContext = null;
			}
		}

		protected void grdCategories_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (DataControlRowType.DataRow == e.Row.RowType)
			{
				int categoryId = (int)DataBinder.Eval(e.Row.DataItem, CATEGORY_ID_COLUMN);
				BaseDataBoundControl grid = e.Row.Cells[1].Controls[1] as BaseDataBoundControl;

				if (grid != null)
				{
					var products = from product in _northwindDataContext.Products
								   where product.CategoryID == categoryId
								   select product;

					BindToSubset(grid, products);
				}
			}
		}

		protected void grdProducts_DataBound(object sender, EventArgs e)
		{
			// sum product count over categories displayed on this page
			const int PRICE_COLUMN_IDX = 1;
			double total = 0;
			GridView grid = (GridView)sender;

			if (grid.Rows.Count > 0)
			{
				foreach (GridViewRow row in grid.Rows)
				{
					total += Double.Parse(row.Cells[PRICE_COLUMN_IDX].Text,
						NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint);
				}

				int cellCount = grid.FooterRow.Cells.Count;
				grid.FooterRow.Cells[0].ColumnSpan = cellCount;

				for (int i = cellCount - 1; i > 0; --i)
				{
					grid.FooterRow.Cells.RemoveAt(i);
				}

				grid.FooterRow.Cells[0].Text = String.Format("Average price: {0:C}", total / grid.Rows.Count);
			}
		}

		#endregion

		#region Implementation

		private void BindToSubset<T>(BaseDataBoundControl control, IEnumerable<T> matches)
		{
			string cacheKey = matches.ToString();

			T[] matchArray = Cache[cacheKey] as T[];

			if (null == matchArray)
			{
				matchArray = matches.ToArray();
				Cache.Insert(cacheKey, matchArray, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(15));
			}

			control.DataSource = matchArray;
			control.DataBind();
		}

		#endregion
	} 
}
