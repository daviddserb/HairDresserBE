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

        Task<IQueryable<Appointment>> GetAllAppointmentsAsync(int pageNumber, int pageSize);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdByDateAsync(string customerId, DateTime appointmentDate);
        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdAsync(string customerId);
        Task<IQueryable<Appointment>> GetInWorkAppointmentsByCustomerIdAsync(string customerId);
        Task<int> CountCustomerAppointmentsLastMonthAsync(string customerId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdByDateAsync(string employeeId, DateTime appointmentDate);
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdAsync(string employeeId);

        Task DeleteAppointmentAsync(int appointmentId);
    }
}
