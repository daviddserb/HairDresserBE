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

        public async Task<IQueryable<Appointment>> ReadAppointmentsAsync()
        {
            return context.Appointments
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee)
                .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
                .ThenInclude(hairServices => hairServices.HairService);
        }

        public async Task<IQueryable<Appointment>> GetAllppointmentsByCustomerIdAsync(int customerId)
        {
            return context.Appointments
                .Where(appointment => appointment.CustomerId == customerId)
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee)
                .Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
                .ThenInclude(hairServices => hairServices.HairService);
        }

        // Asta ajuta cand Customer vrea sa faca un Appointment, si trebuie sa aleaga un Employee si un Date.
        public async Task<IQueryable<Appointment>> GetAllAppointmentsByEmployeeIdByDateAsync(int employeeId, DateTime customerDate)
        {
            return context.Appointments
                // ??? Conteaza unde pun Include, adica in fata de Where sau dupa? Stiu (parca) ca JOIN in SQL se pune inainte de WHERE.
                // ??? aici aveam inainte asa: date => date.StartDate.Date (ca sa iau doar Date-ul din StartDate, nu si Time-ul). Sa verific daca o sa am probleme. Cred ca a trebuit sa schimb pt. ca am pus in clasa din Domain, ? dupa Datetime, ca sa nu-i dea o valoarea default, ca sa aiba rost acel [Required].
                .Where(date => date.StartDate == customerDate.Date)
                .Where(id => id.EmployeeId == employeeId)
                .Include(customers => customers.Customer)
                .Include(employees => employees.Employee);
        }

        // As mai putea sa fac una la fel (ca cea deasupra) unde un employee vrea sa-si vada toate history appointments.

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            throw new NotImplementedException();
        }
    }
}
