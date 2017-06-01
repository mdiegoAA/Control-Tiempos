// ----------------------------------------------------------------------------------------------
// <copyright file="IsDateAfterAttribute.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public sealed class IsDateAfterAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly bool _allowEqualDates;
        private readonly string _testedPropertyName;

        public IsDateAfterAttribute(string testedPropertyName, bool allowEqualDates = false)
        {
            _testedPropertyName = testedPropertyName;
            _allowEqualDates = allowEqualDates;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
                       {
                           ErrorMessage = ErrorMessageString,
                           ValidationType = "isdateafter"
                       };

            rule.ValidationParameters["propertytested"] = _testedPropertyName;
            rule.ValidationParameters["allowequaldates"] = _allowEqualDates;

            yield return rule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(_testedPropertyName);

            if (propertyTestedInfo == null)
                return new ValidationResult($"unknown property {_testedPropertyName}");

            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || !(value is DateTime))
                return ValidationResult.Success;

            if (propertyTestedValue == null || !(propertyTestedValue is DateTime))
                return ValidationResult.Success;

            if ((DateTime) value >= (DateTime) propertyTestedValue)
            {
                if (_allowEqualDates && value == propertyTestedValue)
                    return ValidationResult.Success;

                if ((DateTime) value > (DateTime) propertyTestedValue)
                    return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}