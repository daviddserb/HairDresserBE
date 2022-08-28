using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class InMemoryAppointmentRepository
    {
        public List<Appointment> AppointmentList = new();
        public InMemoryAppointmentRepository()
        {
            // Finished Appointments:
            // Le-am comentat ca sa nu mai stau sa verific daca ora de start si end se incadreaza in functie de programul lui employee.
            //AppointmentList.Add(new Appointment { Id = 1, CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServices = "cut, wash", StartDate = new DateTime(2022, 08, 02, 14, 40, 10), EndDate = new DateTime(2022, 08, 02, 16, 00, 00) });
            //AppointmentList.Add(new Appointment { Id = 2, CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServices = "cut", StartDate = new DateTime(2022, 08, 02, 11, 38, 10), EndDate = new DateTime(2022, 08, 02, 13, 38, 10) });
            //AppointmentList.Add(new Appointment { Id = 3, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 02, 10, 40, 10), EndDate = new DateTime(2022, 08, 08, 11, 20, 10) });
            
            //AppointmentList.Add(new Appointment { Id = 4, CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServices = "cut, wash", StartDate = new DateTime(2022, 08, 09, 14, 40, 10), EndDate = new DateTime(2022, 08, 09, 16, 00, 00) });
            //AppointmentList.Add(new Appointment { Id = 5, CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServices = "cut", StartDate = new DateTime(2022, 08, 09, 10, 38, 10), EndDate = new DateTime(2022, 08, 09, 13, 38, 10) });
            //AppointmentList.Add(new Appointment { Id = 6, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 09, 16, 38, 10), EndDate = new DateTime(2022, 09, 08, 17, 20, 10) });
            //AppointmentList.Add(new Appointment { Id = 7, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 09, 13, 38, 10), EndDate = new DateTime(2022, 09, 08, 15, 20, 10) });
            
            //AppointmentList.Add(new Appointment { Id = 8, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 15, 16, 38, 10), EndDate = new DateTime(2022, 09, 15, 17, 20, 10) });
            //AppointmentList.Add(new Appointment { Id = 9, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 15, 13, 38, 10), EndDate = new DateTime(2022, 09, 15, 15, 20, 10) });

            //AppointmentList.Add(new Appointment { Id = 10, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 17, 16, 38, 10), EndDate = new DateTime(2022, 09, 17, 17, 20, 10) });
            //AppointmentList.Add(new Appointment { Id = 11, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 17, 13, 38, 10), EndDate = new DateTime(2022, 09, 17, 15, 20, 10) });

            //In Work Appointments (Not Finished):
            //AppointmentList.Add(new Appointment { Id = 12, CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServices = "wash, cut", StartDate = new DateTime(2022, 08, 26, 16, 30, 00), EndDate = new DateTime(2022, 08, 26, 18, 00, 00) });
            //AppointmentList.Add(new Appointment { Id = 13, CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServices = "wash", StartDate = new DateTime(2022, 08, 26, 12, 20, 00), EndDate = new DateTime(2022, 08, 26, 14, 00, 00) });
            //AppointmentList.Add(new Appointment { Id = 14, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "cut, dye", StartDate = new DateTime(2022, 08, 26, 14, 00, 00), EndDate = new DateTime(2022, 08, 26, 15, 30, 00) });
            //AppointmentList.Add(new Appointment { Id = 15, CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServices = "dye", StartDate = new DateTime(2022, 08, 26, 10, 30, 00), EndDate = new DateTime(2022, 08, 26, 11, 30, 00) });
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            AppointmentList.Add(appointment);

            // this = pointeaza spre obiectul care apeleaza metoda aceasta metoda (CreateAppointmentAsync).
            //foreach (var apps in this.GetAllAppointments())
            //{
            //    Console.WriteLine($"'{apps.CustomerName}', '{apps.EmployeeName}', '{apps.HairServiceName}', '{apps.StartDate}', '{apps.EndDate}'.");
            //}
        }

        // Asta o sa ajute la Istoricu de Appointments a unui Customer.
        public async Task<IEnumerable<Appointment>> GetAllCustomerAppointmentsAsync(string customerName)
        {
            throw new NotImplementedException();
            //return AppointmentList.Where(obj => obj.CustomerName == customerName);
        }

        // Asta o sa ajute la Read AppointmentsInWork a unui Customer.
        public async Task<IEnumerable<Appointment>> GetAllCustomerAppointmentsInWorkAsync(string customerName)
        {
            return AppointmentList
                .Where(obj => obj.StartDate >= DateTime.Now);
                //.Where(obj => obj.CustomerName == customerName);

        }

        // Asta ajuta cand caut Possible Intervals, pt. un Customer Appointment, dupa selectarea Employee-ului si a Date-ului. 
        public async Task<IEnumerable<Appointment>> GetAppointmentsInWorkAsync(string employeeName, DateTime date)
        {
            throw new NotImplementedException();

            //return AppointmentList
            //    .Where(obj => obj.StartDate.Date == date.Date);
                //.Where(obj => obj.EmployeeName == employeeName);
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
