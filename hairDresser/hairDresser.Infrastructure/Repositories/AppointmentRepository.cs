using hairDresser.Application.Interfaces;
using hairDresser.Domain;
using hairDresser.Domain.Models;
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
            await context.SaveChangesAsync();
        }

        public async Task<IQueryable<Appointment>> GetAllCustomerAppointmentsAsync(string customerName)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Appointment>> GetAllCustomerAppointmentsInWorkAsync(string customerName)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Appointment>> GetAppointmentsInWorkAsync(string employeeName, DateTime date)
        {
            return context.Appointments
                .Where(obj => obj.StartDate.Date == date.Date)
                .Where(obj => obj.EmployeeName == employeeName);
        }

        public async Task<IQueryable<Appointment>> ReadAppointmentsAsync()
        {
            return context.Appointments;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
