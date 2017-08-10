using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace LinqToAdo.Provider.Mappings
{

    /// <summary>
    /// Used as an optimization for serializing
    /// individual <see cref="IDataRecord"/> objects
    /// to a series of models.
    /// </summary>
    internal struct DataMapping
    {
        /// <summary>Creates a new object of the
        /// <see cref="DataMapping"/> structure.</summary>
        /// <param name="property">The <see cref="PropertyInfo"/>
        /// that the value for the column that corresponds
        /// to the <paramref name="ordinal"/> in the
        /// data reader.</param>
        /// <param name="ordinal">The ordinal of the field
        /// to look up.</param>
        /// <param name="propertyType">The <see cref="Type"/>
        /// that is exposed by the <paramref name="property"/>.</param>
        /// <param name="columnType">The <see cref="Type"/>
        /// of the column that is being mapped.</param>
        /// <param name="allowDbNull">True if the column represented by
        /// <paramref name="ordinal"/> is nullable.</param>
        internal DataMapping(int ordinal, PropertyInfo property, Type propertyType,
            Type columnType, bool allowDbNull)
        {
            // The parameters are not null or empty.
            Debug.Assert(property != null);
            Debug.Assert(propertyType != null);
            Debug.Assert(columnType != null);

            // Assign the values used for the mapping.
            Ordinal = ordinal;
            Property = property;
            PropertyType = propertyType;
            ColumnType = columnType;
            AllowDbNull = allowDbNull;
        }

        internal DataMapping(DataRow row, PropertyInfo property, IDataRecord record) :
            this(
                row,
                property,
                record,
                row.Field<int>("ColumnOrdinal"))
        {
        }

        internal DataMapping(DataRow row, PropertyInfo property, IDataRecord record, int ordinal) :
            this(
                ordinal,
                property,
                property.PropertyType,
                record.GetFieldType(ordinal),
                row.Field<bool>("AllowDBNull"))
        {
        }

        internal DataMapping(DataColumn column, PropertyInfo property) :
            this(
                column.Ordinal,
                property,
                property.PropertyType,
                column.DataType,
                column.AllowDBNull)
        {
        }

        internal readonly int Ordinal;
        internal readonly PropertyInfo Property;
        internal readonly Type PropertyType;
        internal readonly Type ColumnType;
        internal readonly bool AllowDbNull;
    }
}
