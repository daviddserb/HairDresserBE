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

        Task<IQueryable<Appointment>> ReadAppointmentsAsync(int pageNumber, int pageSize);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdByDateAsync(string customerId, DateTime appointmentDate);
        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdAsync(string customerId);
        Task<IQueryable<Appointment>> GetInWorkAppointmentsByCustomerIdAsync(string customerId);
        Task<int> GetHowManyAppointmentsCustomerHasInLastMonth(string customerId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdByDateAsync(string employeeId, DateTime appointmentDate);
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdAsync(string employeeId);

        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);

        Task DeleteAppointmentAsync(int appointmentId);
    }
}
