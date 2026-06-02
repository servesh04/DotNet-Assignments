using System;

namespace dotnetapp.Models
{
    public partial class Appointment
    {
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(PatientName) && !string.IsNullOrWhiteSpace(Department) && ConssultationFee >= 0;
        }
    }
}