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

        // This helps to check, when the customer is making an appointment, if the selected interval, by the customer, it's not overlaping with other in work appointments.
        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdByDateAsync(int customerId, DateTime appointmentDate);
        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdAsync(int customerId);
        Task<IQueryable<Appointment>> GetInWorkAppointmentsByCustomerIdAsync(int customerId);
        // Check how many appointments the customer made in the last month (30 days exactly) to see if he went over the limit or not.
        Task<int> GetHowManyAppointmentsCustomerHasInLastMonth(int customerId);

        // This helps to get the free intervals from the employee to make an appointment.
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdByDateAsync(int employeeId, DateTime appointmentDate);
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdAsync(int employeeId);

        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);

        Task DeleteAppointmentAsync(int appointmentId);
    }
}
