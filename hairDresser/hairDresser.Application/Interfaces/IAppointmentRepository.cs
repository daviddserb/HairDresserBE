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
        Task<IQueryable<Appointment>> GetAllCustomerAppointmentsAsync(string customerName);
        Task<IQueryable<Appointment>> GetAllCustomerAppointmentsInWorkAsync(string customerName);
        Task<IQueryable<Appointment>> GetAppointmentsInWorkAsync(string employeeName, DateTime date);
        Task<IQueryable<Appointment>> ReadAppointmentsAsync();
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(Appointment appointment);
    }
}
