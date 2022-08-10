using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        void CreateAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAllAppointmentsByCustomerName(string customerName);
        IEnumerable<Appointment> GetInWorkAppointmentsByEmployeeName(string employeeName);
        IEnumerable<Appointment> GetInWorkAppointmentsByEmployeeNameAndDate(string employeeName, DateTime date);
        IEnumerable<Appointment> GetAllAppointments();
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
