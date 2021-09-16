using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.CustomValidationAttrs
{
    public partial class IsGreaterThan : ValidationAttribute
    {
        public readonly string testedPropertyName;
        public readonly bool allowEqualValues;

        public IsGreaterThan(string testedPropertyName, bool allowEqualValues = false)
        {
            this.testedPropertyName = testedPropertyName;
            this.allowEqualValues = allowEqualValues;
        }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(this.testedPropertyName);
            if (propertyTestedInfo == null)
            {
                return new ValidationResult(string.Format("unknown property {0}", this.testedPropertyName));
            }

            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || propertyTestedValue == null)
            {
                return ValidationResult.Success;
            }

            if (value.GetType() == propertyTestedValue.GetType() && value is IComparable)
            {
                int testVal = ((IComparable)value).CompareTo(propertyTestedValue);
                if (testVal > 0)
                    return ValidationResult.Success;

                if (testVal == 0 && this.allowEqualValues)
                    return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
