using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomUI.Validation
{
    public sealed class AnnotationValidationRule : ValidationRule
    {
        private static IDictionary<Type, IDictionary<string, PropertyValidator>> _validators;
        private BindingExpression _binding;

        static AnnotationValidationRule()
        {
            _validators = new Dictionary<Type, IDictionary<string, PropertyValidator>>();
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string errorMessage = null;

            if (_binding?.ResolvedSource != null)
            {
                Type sourceType = _binding.ResolvedSource.GetType();

                if (!_validators.TryGetValue(sourceType, out IDictionary<string, PropertyValidator> validators))
                {
                    validators = ValidatorFactory.CreateValidators(sourceType);
                    _validators.Add(sourceType, validators);
                }

                if (validators.TryGetValue(_binding.ResolvedSourcePropertyName, out PropertyValidator validator))
                {
                    errorMessage = string.Join(
                        Environment.NewLine,
                        validator.Validate(value));
                }
            }

            return new ValidationResult(
                string.IsNullOrEmpty(errorMessage),
                errorMessage);
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {
            _binding = owner as BindingExpression;
            return base.Validate(value, cultureInfo, owner);
        }
    }
}
