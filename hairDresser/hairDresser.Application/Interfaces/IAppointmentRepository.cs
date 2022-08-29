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
        Task<Appointment> GetAppointmentById(int appointmentId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdAsync(int customerId);
        Task<IQueryable<Appointment>> GetInWorkAppointmentsByCustomerIdAsync(int customerId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdByDateAsync(int employeeId, DateTime customerDate);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int appointmentId);
    }
}
