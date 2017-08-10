using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Northwind_ProductsByCategory : Page
{
	#region Constants

	private const string PRODUCTS_TABLE = "Products";
	private const string CATEGORIES_TABLE = "Categories";
	private const string PROD_CATEGORY_RELATION = "ProductCategory";
	private const string CATEGORY_ID_COLUMN = "CategoryID";
	private const string CATEGORY_NAME_COLUMN = "CategoryName";
	private const string DESCRIPTION_COLUMN = "Description";
	private const string PRODUCT_NAME_COLUMN = "ProductName";
	private const string PRICE_COLUMN = "UnitPrice";
	private const string MAX_FUNCTION = "MAX";
	private const string MIN_FUNCTION = "MIN";
	private const string COUNT_FUNCTION = "COUNT";
	private const string PRODUCTS_CATEGORIES_SESSION_KEY = "NORTHWIND_PRODUCTS";

	private static readonly string[] CATEGORY_FIELDS = new string[] { CATEGORY_ID_COLUMN, CATEGORY_NAME_COLUMN, DESCRIPTION_COLUMN };
	private static readonly string[] PRODUCT_FIELDS = new string[] { CATEGORY_ID_COLUMN, PRODUCT_NAME_COLUMN, PRICE_COLUMN };
	
	#endregion

	#region Event Handlers

	protected void Page_Load(object sender, EventArgs e)
	{
		const int MIN_PRICE = 50;

		BindToSubset(grdCategories, 
			CATEGORIES_TABLE, 
			FormatCategoryChildrenField(MAX_FUNCTION, PRICE_COLUMN),
			ComparisonOperator.MoreThan,
			MIN_PRICE);
	}

	protected void grdCategories_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (DataControlRowType.DataRow == e.Row.RowType)
		{
			int categoryId = (int) DataBinder.Eval(e.Row.DataItem, CATEGORY_ID_COLUMN);
			BaseDataBoundControl grid = e.Row.Cells[1].Controls[1] as BaseDataBoundControl;

			if (grid != null)
			{
				BindToSubset(grid,
					PRODUCTS_TABLE,
					CATEGORY_ID_COLUMN,
					ComparisonOperator.Equals,
					categoryId);
			}
		}
	}

	protected void grdProducts_DataBound(object sender, EventArgs e)
	{
		// sum product count over categories displayed on this page
		const int PRICE_COLUMN_IDX = 1;
		double total = 0;
		GridView grid = (GridView) sender;

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

	private static void Cache_ItemRemoved(string key, object value, CacheItemRemovedReason reason)
	{
	}

	#endregion

	#region Implementation

	private DataSet CategoryProductDataSet
	{
		get
		{
			DataSet retval = Cache[PRODUCTS_CATEGORIES_SESSION_KEY] as DataSet;

			if (null == retval)
			{
				AggregateCacheDependency dependency = new AggregateCacheDependency();
				retval = PrepareDataSet(dependency);
				Cache.Insert(PRODUCTS_CATEGORIES_SESSION_KEY, retval, dependency,
					DateTime.Now.AddMinutes(15), Cache.NoSlidingExpiration, 
					CacheItemPriority.Normal, Cache_ItemRemoved);
			}

			return retval;
		}
	}

	private static DataSet PrepareDataSet(AggregateCacheDependency dependency)
	{

		DataSet ds = new DataSet();
		using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString))
		{
			SqlDataAdapter adapter = new SqlDataAdapter();

			using (SqlCommand cmdCategories = new SqlCommand())
			//SqlCommand cmdCategories = new SqlCommand();
			{
				cmdCategories.Connection = cn;
				adapter.SelectCommand = cmdCategories;
				dependency.Add(FillAdapterWithTableData(adapter, ds, CATEGORIES_TABLE, CATEGORY_FIELDS));
			}

			using (SqlCommand cmdProducts = new SqlCommand())
			//SqlCommand cmdProducts = new SqlCommand();
			{
				cmdProducts.Connection = cn;
				adapter.SelectCommand = cmdProducts;
				dependency.Add(FillAdapterWithTableData(adapter, ds, PRODUCTS_TABLE, PRODUCT_FIELDS));
			}
		}

		//Add relation
		DataRelation rel = new DataRelation(PROD_CATEGORY_RELATION,
			ds.Tables[CATEGORIES_TABLE].Columns[CATEGORY_ID_COLUMN],
			ds.Tables[PRODUCTS_TABLE].Columns[CATEGORY_ID_COLUMN]);

		ds.Relations.Add(rel);

		//Add calculated columns
		AddCalculatedColumnToCategories(ds, "Product Count", typeof(int), COUNT_FUNCTION, CATEGORY_ID_COLUMN);
		AddCalculatedColumnToCategories(ds, "Least Expensive Product", typeof(decimal), MIN_FUNCTION, PRICE_COLUMN);
		AddCalculatedColumnToCategories(ds, "Most Expensive Product", typeof(decimal), MAX_FUNCTION, PRICE_COLUMN);

		return ds;
	}

	private static CacheDependency FillAdapterWithTableData(SqlDataAdapter adapter, DataSet ds, string tableName, string[] columns)
	{
		const string SELECT_FORMAT = "SELECT {0} FROM DBO.{1}";

		adapter.SelectCommand.CommandText = String.Format(SELECT_FORMAT, String.Join(", ", columns), tableName);
		SqlCacheDependency dep = new SqlCacheDependency(adapter.SelectCommand);

		adapter.Fill(ds, tableName);
		return dep;
	}

	private static void AddCalculatedColumnToCategories(DataSet ds, string name, Type type, string function, string childField)
	{
		DataTable table = ds.Tables[CATEGORIES_TABLE];
		table.Columns.Add(name, type,
			FormatCategoryChildrenField(function, childField));
	}

	private static string FormatCategoryChildrenField(string function, string childField)
	{
		const string EXPRESSION_FORMAT = "{0}(Child({1}).{2})";
		return String.Format(EXPRESSION_FORMAT, function, PROD_CATEGORY_RELATION, childField);
	}

	private void BindToSubset<T>(BaseDataBoundControl control, string tableName, string fieldToFilter, ComparisonOperator op, T value) where T : IComparable<T>
	{
		DataTable table = CategoryProductDataSet.Tables[tableName];

		control.DataSource = (fieldToFilter.Contains('(')) ?
			GetDataSourceViaView<T>(table, fieldToFilter, op, value) :
			GetDataSourceViaLinq<T>(table, fieldToFilter, op, value);
		control.DataBind();
	}

	private DataView GetDataSourceViaView<T>(DataTable table, string fieldToFilter, ComparisonOperator op, T value) where T : IComparable<T>
	{
		const string ROW_FILTER_FORMAT = "{0} {1} {2}";
		DataView view = new DataView(table);

		if (!String.IsNullOrEmpty(fieldToFilter))
		{
			view.RowFilter = String.Format(ROW_FILTER_FORMAT,
								fieldToFilter,
								op.AsString(),
								value);
		}
		return view;
	}

	private DataView GetDataSourceViaLinq<T>(DataTable table, string fieldToFilter, ComparisonOperator op, T value) where T : IComparable<T>
	{
		var matches = from row in table.AsEnumerable()
					  where op.Compare<T>(row.Field<T>(fieldToFilter), value)
					  select row;

		return matches.AsDataView();
	}

	#endregion
}
