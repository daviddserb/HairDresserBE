﻿using hairDresser.Domain.Models;

namespace hairDresser.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task CreateAppointmentAsync(Appointment appointment);

        Task<IQueryable<Appointment>> GetAllAppointmentsAsync(int pageNumber, int pageSize);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);

        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdAsync(string customerId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdByDateAsync(string customerId, DateTime appointmentDate);
        Task<IQueryable<Appointment>> GetFinishedAppointmentsByCustomerIdAsync(string customerId);
        Task<IQueryable<Appointment>> GetInWorkAppointmentsByCustomerIdAsync(string customerId);
        Task<int> CountCustomerAppointmentsLastMonthAsync(string customerId);

        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdAsync(string employeeId);
        Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdByDateAsync(string employeeId, DateTime appointmentDate);
        Task<IQueryable<Appointment>> GetFinishedAppointmentsByEmployeeIdAsync(string employeeId);
        Task<IQueryable<Appointment>> GetInWorkAppointmentsByEmployeeIdAsync(string employeeId);

        Task ReviewAppointmentAsync(Review review);

        Task DeleteAppointmentAsync(int appointmentId);
    }
}
