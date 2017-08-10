using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CustomUI.Validation
{
    internal class PropertyValidator
    {
        private readonly IEnumerable<ValidationAttribute> _validators;
        private readonly string _fieldName;

        public PropertyValidator(IEnumerable<ValidationAttribute> validators, string fieldName)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _fieldName = fieldName;
        }

        public IEnumerable<string> Validate(object value)
        {
            return from v in _validators
                   where !v.IsValid(value)
                   select v.FormatErrorMessage(_fieldName);
        }
    }
}
