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
        Task<IQueryable<Appointment>> ReadAppointmentsAsync();
        Task<IQueryable<Appointment>> GetAllAppointmnetsByCustomerIdAsync(int customerId);
        Task<IQueryable<Appointment>> GetAllInWorkAppointmnetsByCustomerIdAsync(int customerId);
        Task<IQueryable<Appointment>> GetAppointmentsInWorkAsync(int employeeId, DateTime customerDate);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(Appointment appointment);
    }
}
