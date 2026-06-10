using System;
using System.ComponentModel.DataAnnotations;
using WebAPIAssignment.Attributes;

namespace WebAPIAssignment.Dtos
{
    public class LeaveRequestCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength =3)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmployeeEmail { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[6-9]\d{9}", ErrorMessage = "Invalid 10-digit Indian mobile number.")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        public string LeaveType {  get; set; } = string.Empty;

        [Required]
        [FutureDate]
        public DateTime StartDate { get; set; }

        [Required]
        [FutureDate]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Reason { get; set; } = string.Empty;
    }
}
