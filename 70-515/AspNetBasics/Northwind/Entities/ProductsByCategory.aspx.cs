using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.Entities;

namespace Northwind.UI.Entities
{
	public partial class ProductsByCategoryPage : Northwind.UI.NorthwindBasePage
	{
		#region Private Constants

		private const string CATEGORY_ID_COLUMN = "CategoryID";
		private const string CATEGORIES_CACHE_KEY = "CategoryEntities";
		private const string PRODUCTS_CACHE_KEY_FORMAT = "ProductEntitiesForCategory{0}";

		#endregion

		#region Private Fields

		NorthwindObjectContext _northwindObjectContext;
		Func<NorthwindObjectContext, int, IQueryable<Product>> _productsByCategoryQuery =
			CompiledQuery.Compile<NorthwindObjectContext, int, IQueryable<Product>>(
				(ctx, categoryId) => from p in ctx.Products
									 where p.Category.CategoryID == categoryId
									 select p);

		#endregion

		#region Event Handlers

		protected void Page_Load(object sender, EventArgs e)
		{
			_northwindObjectContext = new NorthwindObjectContext(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString);

			var categories = from category in _northwindObjectContext.Categories
							 join product in _northwindObjectContext.Products on category.CategoryID equals product.Category.CategoryID into gj
							 where gj.Max(p => p.UnitPrice) > 60
							 select new
							 {
								 CategoryID = category.CategoryID,
								 Name = category.CategoryName,
								 ProductCount = gj.Count(),
								 Description = category.Description
							 };

			BindToSubset(grdCategories, categories, CATEGORIES_CACHE_KEY);
		}

		protected void grdCategories_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (DataControlRowType.DataRow == e.Row.RowType)
			{
				int categoryId = (int)DataBinder.Eval(e.Row.DataItem, CATEGORY_ID_COLUMN);
				BaseDataBoundControl grid = e.Row.Cells[1].Controls[1] as BaseDataBoundControl;

				if (grid != null)
				{
					var products = _productsByCategoryQuery(_northwindObjectContext, categoryId);
					BindToSubset(grid, products, String.Format(PRODUCTS_CACHE_KEY_FORMAT, categoryId));
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

		private void BindToSubset<T>(BaseDataBoundControl control, IEnumerable<T> matches, string cacheKey)
		{
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
