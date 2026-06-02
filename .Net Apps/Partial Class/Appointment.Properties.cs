using System;

namespace dotnetapp.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Department { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal ConsultationFee { get; set; }
        public string Status { get; set; }
    }
}