using System;
using System.Data;
using System.Reflection;

namespace LinqToAdo.Provider.Mappings
{
    internal sealed class DataRowAccessor : BaseDataAccessor
    {
        /// <summary>
        /// The <see cref="MethodInfo"/> which will
        /// be used to create the closed generic
        /// <see cref="System.Data.DataRowExtensions.Field{T}(DataRow, int)"/>
        /// method.
        /// </summary>
        private static readonly MethodInfo _fieldGenericMethodInfo;

        private readonly DataRow _row;

        /// <summary>
        /// Initializes static members of <see cref="DataRowAccessor"/>
        /// </summary>
        static DataRowAccessor()
        {
            _fieldGenericMethodInfo = typeof(System.Data.DataRowExtensions).
                GetMethod(
                    "Field",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[] { typeof(DataRow), typeof(int) },
                    null);
        }

        public DataRowAccessor(DataRow row)
        {
            _row = row;
        }

        protected override object ReadColumnValue(Type columnType, DataMapping mapping)
        {
            // Create the method info for the Field
            // extension method.
            MethodInfo methodInfo = _fieldGenericMethodInfo.MakeGenericMethod(columnType);

            // Now invoke to get the value.
            object value = methodInfo.Invoke(
                null,
                new object[]
                {
                    _row,
                    mapping.Ordinal
                });

            return value;
        }

        protected override DataMapping? CreateMapping(PropertyInfo property)
        {
            DataColumn column = _row.Table.Columns[property.Name];

            return column != null ?
                new DataMapping(column, property) :
                (DataMapping?)null;
        }
    }
}
