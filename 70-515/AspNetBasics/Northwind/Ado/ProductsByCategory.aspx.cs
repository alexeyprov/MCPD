using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Northwind.UI.Ado
{
    public partial class ProductsByCategoryPage : Northwind.UI.NorthwindBasePage
    {
        #region Constants

        private const string PRODUCTS_TABLE = "Products";
        private const string CATEGORIES_TABLE = "Categories";
        private const string PROD_CATEGORY_RELATION = "ProductCategory";
        private const string CATEGORY_ID_COLUMN = "CategoryID";
        private const string CATEGORY_NAME_COLUMN = "CategoryName";
        private const string PRODUCT_NAME_COLUMN = "ProductName";
        private const string PRICE_COLUMN = "UnitPrice";
        private const string MAX_FUNCTION = "MAX";
        private const string MIN_FUNCTION = "MIN";
        private const string COUNT_FUNCTION = "COUNT";
        private const string PRODUCTS_CATEGORIES_SESSION_KEY = "NORTHWIND_PRODUCTS";

        private static readonly string[] CATEGORY_FIELDS = new string[] { CATEGORY_ID_COLUMN, CATEGORY_NAME_COLUMN };
        private static readonly string[] PRODUCT_FIELDS = new string[] { CATEGORY_ID_COLUMN, PRODUCT_NAME_COLUMN, PRICE_COLUMN };

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            const int MIN_PRICE = 50;

            BindToSubset(grdCategories,
                CATEGORIES_TABLE,
                CalculatedColumnMetadata.FormatChildField(MAX_FUNCTION, PRICE_COLUMN, PROD_CATEGORY_RELATION),
                ComparisonOperator.MoreThan,
                MIN_PRICE);
        }

        protected void grdCategories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (DataControlRowType.DataRow == e.Row.RowType)
            {
                int categoryId = (int)DataBinder.Eval(e.Row.DataItem, CATEGORY_ID_COLUMN);
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
                    using (DataContainer data = PrepareDataSet())
                    {
                        retval = data.Dataset;

                        Cache.Insert(
                            PRODUCTS_CATEGORIES_SESSION_KEY,
                            retval, 
                            data.Dependency,
                            DateTime.Now.AddMinutes(15),
                            Cache.NoSlidingExpiration,
                            CacheItemPriority.Normal, 
                            Cache_ItemRemoved);
                    }
                }

                return retval;
            }
        }

        private static DataContainer PrepareDataSet()
        {
            DataContainer data = new DataContainer(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);

            data.AppendTable(CATEGORIES_TABLE, CATEGORY_FIELDS,
                new CalculatedColumnMetadata("Product Count", typeof(int), COUNT_FUNCTION, CATEGORY_ID_COLUMN, PROD_CATEGORY_RELATION),
                new CalculatedColumnMetadata("Least Expensive Product", typeof(decimal), MIN_FUNCTION, PRICE_COLUMN, PROD_CATEGORY_RELATION),
                new CalculatedColumnMetadata("Most Expensive Product", typeof(decimal), MAX_FUNCTION, PRICE_COLUMN, PROD_CATEGORY_RELATION));

            data.AppendTable(PRODUCTS_TABLE, PRODUCT_FIELDS);

            data.AppendRelation(CATEGORIES_TABLE, PRODUCTS_TABLE, CATEGORY_ID_COLUMN, CATEGORY_ID_COLUMN, PROD_CATEGORY_RELATION);

            data.InsertCalculatedColumns();

            return data;
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

        #region Helper Classes

        /// <summary>
        /// Information about calculated data table column
        /// </summary>
        private class CalculatedColumnMetadata
        {
            private const string EXPRESSION_FORMAT = "{0}(Child({1}).{2})";

            private string _name;
            private Type _type;
            private string _expression;

            public CalculatedColumnMetadata(string name, Type type, string aggregateFunction, string sourceColumn, string relationName)
            {
                _name = name;
                _type = type;
                _expression = FormatChildField(aggregateFunction, sourceColumn, relationName);
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public Type Type
            {
                get
                {
                    return _type;
                }
            }

            public string Expression
            {
                get
                {
                    return _expression;
                }
            }

            public static string FormatChildField(string aggregateFunction, string sourceColumn, string relationName)
            {
                return String.Format(EXPRESSION_FORMAT, aggregateFunction,
                        relationName, sourceColumn);
            }
        }

        /// <summary>
        /// Storage class for data set and its dependencies
        /// </summary>
        private class DataContainer : IDisposable
        {
            private const string SELECT_FORMAT = "SELECT {0} FROM [dbo].[{1}]";

            private DataSet _dataset;
            private AggregateCacheDependency _dependency;
            private SqlDataAdapter _adapter;
            private SqlConnection _connection;

            private Dictionary<string, CalculatedColumnMetadata[]> _calculatedColumns;

            public DataContainer(string connectionString)
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                _dataset = new DataSet();
                _dependency = new AggregateCacheDependency();
                _adapter = new SqlDataAdapter();

                _calculatedColumns = new Dictionary<string, CalculatedColumnMetadata[]>();
            }

            public void Dispose()
            {
                _connection.Dispose();
            }

            public CacheDependency Dependency
            {
                get
                {
                    return _dependency;
                }
            }

            public DataSet Dataset
            {
                get
                {
                    return _dataset;
                }
            }

            public void AppendTable(string tableName, string[] columnNames, params CalculatedColumnMetadata[] calculatedCols)
            {
                using (SqlCommand cmd = new SqlCommand
                    {
                        CommandText = String.Format(SELECT_FORMAT, String.Join(", ", columnNames), tableName),
                        Connection = _connection
                    })
                {
                    SqlCacheDependency dep = new SqlCacheDependency(cmd);

                    _adapter.SelectCommand = cmd;
                    _adapter.Fill(_dataset, tableName);

                    _dependency.Add(dep);
                }

                _calculatedColumns[tableName] = calculatedCols;
            }

            public void AppendRelation(string parentTable, string childTable, string parentKey, string childKey, string relationName)
            {
                DataRelation rel = new DataRelation(relationName,
                    _dataset.Tables[parentTable].Columns[parentKey],
                    _dataset.Tables[childTable].Columns[childKey]);

                _dataset.Relations.Add(rel);
            }

            public void InsertCalculatedColumns()
            {
                foreach (string tableName in _calculatedColumns.Keys)
                {
                    CalculatedColumnMetadata[] calculatedCols = _calculatedColumns[tableName];

                    if (calculatedCols != null)
                    {
                        DataTable table = _dataset.Tables[tableName];

                        foreach (CalculatedColumnMetadata columnInfo in calculatedCols)
                        {
                            table.Columns.Add(columnInfo.Name, columnInfo.Type, columnInfo.Expression);
                        }
                    }
                }

                _calculatedColumns.Clear();
            }
        }

        #endregion
    }
}
