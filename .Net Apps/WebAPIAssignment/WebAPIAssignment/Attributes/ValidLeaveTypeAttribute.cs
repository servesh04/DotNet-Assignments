using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebAPIAssignment.Attributes
{
    public class ValidLeaveTypeAttribute : ValidationAttribute
    {
        private readonly string[] _allowedTypes = { "Sick", "Casual", "Earned" };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is string leaveType && _allowedTypes.Contains(leaveType))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("LeaveType must be Sick, Casual, Earned");
        }
    }
}
