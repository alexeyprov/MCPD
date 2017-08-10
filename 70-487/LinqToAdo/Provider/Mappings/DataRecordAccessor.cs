using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace LinqToAdo.Provider.Mappings
{
    internal sealed class DataRecordAccessor : BaseDataAccessor
    {
        private readonly IDataRecord _record;
        private readonly IDictionary<string, DataRow> _schema;

        public DataRecordAccessor(IDataRecord record, DataTable schema)
        {
            _record = record;
            _schema = schema.AsEnumerable().ToDictionary(
                r => r.Field<string>("ColumnName"));
        }

        protected override object ReadColumnValue(Type columnType, DataMapping mapping)
        {
            // Get the value from the reader.
            object value = _record.GetValue(mapping.Ordinal);

            // If the value is db null, then
            // set the value to null.
            if (value == DBNull.Value)
            {
                value = null;
            }

            if (mapping.ColumnType.IsValueType)
            {
                // If the value is null then just return a new object
                // of the null item.
                return (value == null) ?
                    // Return a new nullable.
                    Activator.CreateInstance(columnType) :
                    // Return the activator, passing the value.
                    Activator.CreateInstance(columnType, value);
            }

            return value;
        }

        protected override DataMapping? CreateMapping(PropertyInfo property)
        {
            DataRow schemaRow;
            return _schema.TryGetValue(property.Name, out schemaRow) ?
                new DataMapping(schemaRow, property, _record) :
                (DataMapping?)null;
        }
    }
}
