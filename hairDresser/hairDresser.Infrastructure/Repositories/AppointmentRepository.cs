using hairDresser.Application.Interfaces;
using hairDresser.Domain;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DataContext context;

        public AppointmentRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            await context.Appointments.AddAsync(appointment);
        }

        public async Task<IQueryable<Appointment>> GetAllAppointmentsAsync(int pageNumber, int PageSize)
        {
            // BEFORE:
            //return context.Appointments
            //    .Include(customers => customers.Customer)
            //    .Include(employees => employees.Employee)
            //    .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
            //    .ThenInclude(hairServices => hairServices.HairService)
            //    .Skip((pageNumber - 1) * PageSize)
            //    .Take(PageSize);
            // AFTER:
            return context.Appointments
                // BEFORE:
                //.Include(users => users.User)
                // AFTER:
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee)
                .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
                .ThenInclude(hairServices => hairServices.HairService)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize);

        }
        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            // BEFORE:
            //return await context.Appointments
            //    .Include(customers => customers.Customer)
            //    .Include(employees => employees.Employee)
            //    .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
            //    .ThenInclude(hairServices => hairServices.HairService)
            //    .FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);
            // AFTER:
            return await context.Appointments

                // BEFORE:
                //.Include(users => users.User)
                // AFTER:
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee)
                .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
                .ThenInclude(hairServices => hairServices.HairService)
                .FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);
        }

        public async Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdByDateAsync(string customerId, DateTime appointmentDate)
        {
            return context.Appointments
                .Where(date => date.StartDate.Date == appointmentDate.Date)
                .Where(id => id.CustomerId == customerId)
                .OrderBy(date => date.StartDate);
        }

        public async Task<IQueryable<Appointment>> GetAllAppointmentsByCustomerIdAsync(string customerId)
        {
            // BEFORE:
            //return context.Appointments
            //    .Where(appointment => appointment.CustomerId == customerId)
            //    .Include(customers => customers.Customer)
            //    .Include(employees => employees.Employee)
            //    .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
            //    .ThenInclude(hairServices => hairServices.HairService);
            // AFTER:
            return context.Appointments
                .Where(appointment => appointment.CustomerId == customerId)
                // BEFORE:
                //.Include(users => users.User)
                // AFTER:
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee)
                .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
                .ThenInclude(hairServices => hairServices.HairService);
        }

        // Pt. appointment-urile din viitor (neterminate) ale unui customer.
        public async Task<IQueryable<Appointment>> GetInWorkAppointmentsByCustomerIdAsync(string customerId)
        {
            // BEFORE:
            //return context.Appointments
            //    .Where(appointment => appointment.CustomerId == customerId)
            //    .Where(date => date.StartDate >= DateTime.Now.Date)
            //    .Include(customers => customers.Customer)
            //    .Include(employees => employees.Employee)
            //    .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
            //    .ThenInclude(hairServices => hairServices.HairService);
            // AFTER:
            return context.Appointments
                .Where(appointment => appointment.CustomerId == customerId)
                .Where(date => date.StartDate >= DateTime.Now.Date)
                // BEFORE:
                //.Include(users => users.User)
                // AFTER:
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee)
                .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
                .ThenInclude(hairServices => hairServices.HairService);
        }
        public async Task<int> GetHowManyAppointmentsCustomerHasInLastMonth(string customerId)
        {
            return context.Appointments
                .Where(appointment => appointment.CustomerId == customerId)
                .Where(date => date.StartDate >= DateTime.Today.AddDays(-30))
                .Count();
        }

        public async Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdByDateAsync(string employeeId, DateTime appointmentDate)
        {
            return context.Appointments
                .Where(date => date.StartDate.Date == appointmentDate.Date)
                .Where(id => id.EmployeeId == employeeId)
                // BEFORE:
                //.Include(customers => customers.Customer)
                //.Include(employees => employees.Employee);

                // AFTER:
                // BEFORE:
                //.Include(users => users.User);
                // AFTER:
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee);
        }
        public async Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdAsync(string employeeId)
        {
            // BEFORE:
            //return context.Appointments
            //    .Where(appointment => appointment.EmployeeId == employeeId)
            //    .Include(customers => customers.Customer)
            //    .Include(employees => employees.Employee)
            //    .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
            //    .ThenInclude(hairServices => hairServices.HairService);
            // AFTER:
            return context.Appointments
                .Where(appointment => appointment.EmployeeId == employeeId)
                // BEFORE:
                //.Include(users => users.User)
                // AFTER:
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee)
                .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
                .ThenInclude(hairServices => hairServices.HairService);
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            context.Appointments.Update(appointment);
            return appointment;
        }

        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);

            //context.Appointments.Remove(appointment); //before

            //after: (We don't want to delete it, but instead soft deleted.
            appointment.isDeleted = DateTime.Now;
            context.Appointments.Update(appointment);
        }
    }
}
