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
        Task<IQueryable<Appointment>> GetAllppointmentsByCustomerIdAsync(int customerId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdByDateAsync(int employeeId, DateTime customerDate);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int appointmentId);
    }
}
