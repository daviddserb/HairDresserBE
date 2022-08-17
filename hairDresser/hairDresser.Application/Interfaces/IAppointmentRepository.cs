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
        Task CreateAppointmentAsync(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAllCustomerAppointmentsAsync(string customerName);
        Task<IEnumerable<Appointment>> GetAllCustomerAppointmentsInWorkAsync(string customerName);
        Task<IEnumerable<Appointment>> GetInWorkAppointmentsAsync(string employeeName, DateTime date);
        Task<IEnumerable<Appointment>> ReadAppointmentsAsync();
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(Appointment appointment);
    }
}
