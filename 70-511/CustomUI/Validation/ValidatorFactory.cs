using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CustomUI.Validation
{
    internal static class ValidatorFactory
    {
        public static IDictionary<string, PropertyValidator> CreateValidators(Type type)
        {
            MetadataTypeAttribute metadata = (type ?? throw new ArgumentNullException(nameof(type)))
                .GetCustomAttributes(typeof(MetadataTypeAttribute), false)
                .OfType<MetadataTypeAttribute>()
                .FirstOrDefault();

            Type buddyType = metadata?.MetadataClassType;

            return CollectTypeAttributes(buddyType ?? type)
                .ToDictionary(
                    p => p.Key,
                    p => new PropertyValidator(p.Value, p.Key));
        }

        private static IEnumerable<KeyValuePair<string, IEnumerable<ValidationAttribute>>> CollectTypeAttributes(
            Type type)
        {
            return from PropertyInfo p in type.GetProperties()
                   select new KeyValuePair<string, IEnumerable<ValidationAttribute>>(
                       p.Name,
                       p.GetCustomAttributes<ValidationAttribute>(false));
        }
    }
}
