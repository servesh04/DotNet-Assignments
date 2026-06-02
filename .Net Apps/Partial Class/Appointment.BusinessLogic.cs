using System;

namespace dotnetapp.Models
{
    public partial class Appointment
    {
        public void CompleteAppointment()
        {
            if (Status == "Scheduled" && AppointmentDate <= DateTime.Now)
            {
                Status = "Completed";
            }
        }

        public bool IsUpcoming()
        {
            return Status == "Scheduled" && AppointmentDate > DateTime.Now;
        }
    }
}
