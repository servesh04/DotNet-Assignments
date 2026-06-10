using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebAPIAssignment.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateTime date && date.Date > DateTime.Today)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"{validationContext.DisplayName} must be a future date.");
        }
    }
}
