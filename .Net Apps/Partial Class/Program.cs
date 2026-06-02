using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
class Program
{
    public static void Main(string[] args)
    {
        List<Appointment> appointments = new List<Appointment>
        {
            new Appointment { Id = 1, PatientName = "Alice", Department = "Cardiology", AppointmentDate = DateTime.Now.AddDays(2), ConsultationFee = 600, Status = "Scheduled" },
            new Appointment { Id = 2, PatientName = "Bob", Department = "Neurology", AppointmentDate = DateTime.Now.AddDays(-2), ConsultationFee = 450, Status = "Completed" },
            new Appointment { Id = 3, PatientName = "Charlie", Department = "Cardiology", AppointmentDate = DateTime.Now.AddDays(-1), ConsultationFee = 700, Status = "Completed" },
            new Appointment { Id = 4, PatientName = "David", Department = "Orthopedics", AppointmentDate = DateTime.Now.AddDays(5), ConsultationFee = 300, Status = "Scheduled" },
            new Appointment { Id = 5, PatientName = "Eve", Department = "Cardiology", AppointmentDate = DateTime.Now.AddDays(1), ConsultationFee = 550, Status = "Scheduled" }
        };



        Print("All Appointments", appointments);

        var scheduled = appointments.Where(app => app.Status == "Schedules");
        Print("Scheduled", scheduled);

        var completed = appointments.Where(app => app.Status == "Completed");
        Print("Completed", completed);

        var cardiology = appointments.Where(app => app.Department == "Cardiology");
        Print("Cardiology", cardiology);

        var highFee = appointments.Where(app => app.ConsultationFee > 500);
        Print("Consultation Fee > 500", highFee);

        var sortedByDate = appointments.OrderBy(app => app.AppointmentDate);
        Print("Sorted by Date", sortedByDate);

        var searchName = appointments.Where(app => app.PatientName.Equals("Alice",StringComparison.OrdinalIgnoreCase));
        Print("Search Result for Alice", searchName);

        Console.WriteLine("--- Count by Status ---");
        var countByStatus = appointments.GroupBy(app => app.Status);
        foreach(var group in countByStatus)
        {
            Console.WriteLine($"{group.Key}: {group.Count()}");
        }

        decimal totalRevenue = appointments.Where(app => app.Status == "Completed").Sum(app => app.ConsultationFee);
        Console.WriteLine($"Total Revenue : {totalRevenue}");

        double avgFee = (double)appointments.Average(app => app.ConsultationFee);
        Console.WriteLine($"Avg Consultation Fee: {avgFee}");

        var upcoming = appointments.Where(app => app.IsUpComing());
        Print("Upcoming Appointments", upcoming);


    }
    public static void Print(string title, IEnumerable<Appointment> list)
    {
        Console.WriteLine(title);
        foreach (var app in list)
        {
            Console.WriteLine($"{app.Id} {app.PatientName} - {app.Department} | {app.AppointmentDate.ToString()} | {app.ConsultationFee} | {app.Status}");
        }
    }
}