using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LinqToAdo.Provider.Mappings
{
    internal abstract class BaseDataAccessor : IDataAccessor
    {
        #region IDataAccessor Members

        IEnumerable<DataMapping> IDataAccessor.CreateMappings<T>()
        {
            IEnumerable<DataMapping> mappings =
                from property in typeof(T).GetProperties()
                let mapping = CreateMapping(property)
                where mapping.HasValue
                select mapping.Value;

            return mappings.ToArray();
        }

        T IDataAccessor.UpdateModel<T>(T model, IEnumerable<DataMapping> mappings)
        {
            Debug.Assert(model != null);
            Debug.Assert(mappings != null);

            // Cycle through the mappings and assign.
            foreach (DataMapping mapping in mappings)
            {
                // Get the value.
                object value = GetColumnValue(mapping);

                // Convert the value to the property type.
                object convertedValue = Convert(value, mapping.PropertyType);

                // Assign the property.
                mapping.Property.SetValue(model, convertedValue, null);
            }

            // The model is populated.
            return model;

        }

        #endregion

        protected virtual object GetColumnValue(DataMapping mapping)
        {
            Type columnType = mapping.ColumnType;

            // If the type is a struct and the column allows
            // db nulls then convert to a nullable
            // of that type.
            if (columnType.IsValueType && mapping.AllowDbNull)
            {
                // Set the type to a nullable type.
                columnType = typeof(Nullable<>).MakeGenericType(columnType);
            }

            return ReadColumnValue(columnType, mapping);
        }

        protected abstract object ReadColumnValue(Type columnType, DataMapping mapping);

        protected abstract DataMapping? CreateMapping(PropertyInfo property);

        #region Implementation

        /// <summary>Converts a <paramref name="value"/>
        /// from its type to a specified
        /// <paramref name="type"/>.</summary>
        /// <param name="value">The value to
        /// convert to an object of <paramref name="type"/>.</param>
        /// <param name="type">The <see cref="Type"/>
        /// to convert <paramref name="value"/>
        /// to.</param>
        /// <returns>The <paramref name="value"/>
        /// converted to the specified
        /// <paramref name="type"/>.</returns>
        private static object Convert(object value, Type type)
        {
            // The type must not be null.
            Debug.Assert(type != null);

            // If the value is null, return null, what
            // else can be done?
            if (value == null)
                return value;

            // If the value is DBNull then return null.
            if (value == DBNull.Value)
                return null;

            // Get the type of the value if there is one.
            Type valueType = value.GetType();

            // If the value type and the type are the same
            // then return the value.
            object checkedValue = CheckConversionTypes(valueType, value, type);

            // If the value is not null, return the value.
            if (checkedValue != null)
                return checkedValue;

            // Pre-format the value.
            value = PreFormatValue(value, type);

            // Get the value type over again.
            valueType = value.GetType();

            // Perform the check again.
            if ((checkedValue = CheckConversionTypes(valueType, value, type)) != null)
                return checkedValue;

            // As a last resort, submit to type converters.  Get the
            // converter for the type first.
            TypeConverter converter = TypeDescriptor.GetConverter(type);

            // If the converter can convert from
            // the type return the converted value.
            if (converter.CanConvertFrom(valueType))
            {
                // Return the converted value.
                return converter.ConvertFrom(value);
            }

            // It can't convert so try the converter for the value type
            // and see if that works.
            converter = TypeDescriptor.GetConverter(valueType);

            // If the value can be converted then do that.
            if (converter.CanConvertTo(type))
            {
                // Return the converted value.
                return converter.ConvertTo(value, type);
            }

            // if we are converting from a nullable type, but the underlying types are compatible, try using the underlying
            bool isNullableType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            if (isNullableType)
            {
                type = type.GetGenericArguments().Single();

                if (converter.CanConvertTo(type))
                {
                    // Return the converted value.
                    return converter.ConvertTo(value, type);
                }
            }

            // Throw an exception.
            throw new InvalidOperationException(
                string.Format(
                    "No conversion exists from {0} to {1}.",
                    valueType,
                    type));
        }

        /// <summary>Checks the from and the to conversion types
        /// to see if conversion is needed, extracting the value if
        /// nullable types are encountered.</summary>
        /// <param name="fromType">The type that is being converted from.</param>
        /// <param name="value">The value being converted.</param>
        /// <param name="toType">The type that is being converted to.</param>
        /// <returns>The value, if it does not require conversion, or
        /// null, if it does.</returns>
        private static object CheckConversionTypes(Type fromType, object value, Type toType)
        {
            Debug.Assert(fromType != null);
            Debug.Assert(toType != null);
            Debug.Assert(value != null);

            // If the from and to types are equal, then return
            // the value.
            if (toType == fromType)
                return value;

            // Is the value being converted to a nullable type?
            bool isNullableType = toType.IsGenericType &&
                toType.GetGenericTypeDefinition() == typeof(Nullable<>);

            // Special-case the logic when this is
            // a nullable type.
            if (isNullableType)
            {
                // If the type parameter of the
                // type is the same as
                // the type of the value, return
                // the value, as this conversion
                // is explicit.
                if (toType.GetGenericArguments().Single() == fromType)
                    return value;
            }

            // Check the value type to see if it is nullable.  If it is
            // and the type is equal to the type parameter
            // then return the value (since we know it is not null
            // anyways).  Find out if it is a nullable type
            // first.
            isNullableType = fromType.IsGenericType &&
                fromType.GetGenericTypeDefinition() == typeof(Nullable<>);

            // If the value type is nullable, then check the type
            // parameter.
            if (isNullableType && fromType.GetGenericArguments().Single() == toType)
            {
                // Return the value on the nullable, use dynamic to
                // help with reflection.
                dynamic nullable = value;

                // Return the value.
                return nullable.Value;
            }

            // Return null, requires conversion.
            return null;
        }

        /// <summary>Pre formats the value before conversion takes
        /// place.</summary>
        /// <remarks>Right now, this handles the case where numbers
        /// are treated as boolean values.</remarks>
        /// <param name="value">The value that needs to be converted.</param>
        /// <param name="type">The type that is being converted to.</param>
        /// <returns>The preformatted value.</returns>
        private static object PreFormatValue(object value, Type type)
        {
            // The value is not null or DBNull and
            // the type is not null.
            Debug.Assert(value != null);
            Debug.Assert(value != DBNull.Value);
            Debug.Assert(type != null);

            // The output double value.
            double number;

            // Perform checks on the value.  If the value is a string
            // and convertable to a number and the destination type is
            // boolean, then convert the value to a boolean.
            if ((type == typeof(bool) || type == typeof(bool?)) &&
                (value.GetType() == typeof(string) && double.TryParse(value as string, out number)))
            {
                // If the number is 0, return false, otherwise, return true.
                return number != 0.0f;
            }

            // Return the value.
            return value;
        }

        #endregion
    }
}
