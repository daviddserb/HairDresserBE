using hairDresser.Application.Interfaces;
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
        public List<Appointment> AppointmentList = new();
        public AppointmentRepository()
        {
            // Finished Appointments:
            AppointmentList.Add(new Appointment { CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServices = "cut, wash", StartDate = new DateTime(2022, 08, 02, 14, 40, 10), EndDate = new DateTime(2022, 08, 02, 16, 00, 00) });
            AppointmentList.Add(new Appointment { CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServices = "cut", StartDate = new DateTime(2022, 08, 02, 11, 38, 10), EndDate = new DateTime(2022, 08, 02, 13, 38, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 02, 10, 40, 10), EndDate = new DateTime(2022, 08, 08, 11, 20, 10) });
            
            AppointmentList.Add(new Appointment { CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServices = "cut, wash", StartDate = new DateTime(2022, 08, 09, 14, 40, 10), EndDate = new DateTime(2022, 08, 09, 16, 00, 00) });
            AppointmentList.Add(new Appointment { CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServices = "cut", StartDate = new DateTime(2022, 08, 09, 10, 38, 10), EndDate = new DateTime(2022, 08, 09, 13, 38, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 09, 16, 38, 10), EndDate = new DateTime(2022, 09, 08, 17, 20, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 09, 13, 38, 10), EndDate = new DateTime(2022, 09, 08, 15, 20, 10) });
            
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 15, 16, 38, 10), EndDate = new DateTime(2022, 09, 15, 17, 20, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 15, 13, 38, 10), EndDate = new DateTime(2022, 09, 15, 15, 20, 10) });

            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 17, 16, 38, 10), EndDate = new DateTime(2022, 09, 17, 17, 20, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 17, 13, 38, 10), EndDate = new DateTime(2022, 09, 17, 15, 20, 10) });

            //In Work Appointments (Not Finished):
            AppointmentList.Add(new Appointment { CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServices = "cut, wash", StartDate = new DateTime(2022, 08, 19, 16, 30, 00), EndDate = new DateTime(2022, 08, 19, 18, 00, 00) });
            AppointmentList.Add(new Appointment { CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServices = "cut, wash", StartDate = new DateTime(2022, 08, 19, 12, 20, 00), EndDate = new DateTime(2022, 08, 19, 14, 00, 00) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "cut, wash", StartDate = new DateTime(2022, 08, 19, 15, 38, 10), EndDate = new DateTime(2022, 08, 19, 17, 20, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "cut, wash", StartDate = new DateTime(2022, 08, 19, 10, 38, 10), EndDate = new DateTime(2022, 08, 19, 13, 20, 10) });
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            AppointmentList.Add(appointment);

            // this -> pointeaza spre obiectul (in cazul nostru appRepo) care apeleaza metoda (in cazul nostru GetAllAppointments()).
            //foreach (var apps in this.GetAllAppointments())
            //{
            //    Console.WriteLine($"'{apps.CustomerName}', '{apps.EmployeeName}', '{apps.HairServiceName}', '{apps.StartDate}', '{apps.EndDate}'.");
            //}
        }

        // Asta o sa ajute la Istoricu de Appointments a unui Customer.
        public async Task<IEnumerable<Appointment>> GetAllCustomerAppointmentsAsync(string customerName)
        {
            return AppointmentList.Where(obj => obj.CustomerName == customerName);
        }

        // Asta o sa ajute la Read AppointmentsInWork from a Customer.
        public async Task<IEnumerable<Appointment>> GetAllCustomerAppointmentsInWorkAsync(string customerName)
        {
            return AppointmentList
                .Where(obj => obj.StartDate >= DateTime.Now)
                .Where(obj => obj.CustomerName == customerName);

        }

        public async Task<IEnumerable<Appointment>> GetInWorkAppointmentsAsync(string employeeName, DateTime date)
        {
            return AppointmentList
                .Where(obj => obj.StartDate.Date == date.Date)
                .Where(obj => obj.EmployeeName == employeeName);
        }

        public async Task<IEnumerable<Appointment>> ReadAppointmentsAsync()
        {
            return AppointmentList;
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
