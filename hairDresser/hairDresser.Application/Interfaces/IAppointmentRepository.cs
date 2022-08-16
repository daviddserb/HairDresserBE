using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        void CreateAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAllCustomerAppointments(string customerName);
        IEnumerable<Appointment> GetAllCustomerAppointmentsInWork(string customerName);
        IEnumerable<Appointment> GetInWorkAppointments(string employeeName, DateTime date);
        IEnumerable<Appointment> GetAllAppointments();
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
