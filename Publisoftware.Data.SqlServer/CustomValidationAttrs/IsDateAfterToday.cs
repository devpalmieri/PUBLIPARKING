using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.CustomValidationAttrs
{
    public partial class IsDateAfterToday : ValidationAttribute
    {
        public readonly bool allowEqualDates;

        public IsDateAfterToday(bool allowEqualDates = false)
        {
            this.allowEqualDates = allowEqualDates;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is DateTime))
            {
                return ValidationResult.Success;
            }

            if ((DateTime)value >= DateTime.Now.Date)
            {
                if (this.allowEqualDates)
                {
                    return ValidationResult.Success;
                }
                if ((DateTime)value > DateTime.Now.Date)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
