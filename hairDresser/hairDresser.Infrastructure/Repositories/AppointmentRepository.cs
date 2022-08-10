using hairDresser.Domain.Interfaces;
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
        public List<Appointment> AppointmentList = new List<Appointment>();
        public AppointmentRepository()
        {
            // Finished Appointments:
            AppointmentList.Add(new Appointment {CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServiceName = "cut, wash", StartDate = new DateTime(2022, 08, 02, 14, 40, 10), EndDate = new DateTime(2022, 08, 02, 16, 00, 00) });
            AppointmentList.Add(new Appointment {CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServiceName = "cut", StartDate = new DateTime(2022, 08, 02, 11, 38, 10), EndDate = new DateTime(2022, 08, 02, 13, 38, 10) });
            AppointmentList.Add(new Appointment {CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "dye", StartDate = new DateTime(2022, 08, 02, 10, 40, 10), EndDate = new DateTime(2022, 08, 08, 11, 20, 10) });

            //In Work (Not Finished) Appointments:
            //Current Day:
            AppointmentList.Add(new Appointment { CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServiceName = "cut, wash", StartDate = new DateTime(2022, 08, 09, 14, 40, 10), EndDate = new DateTime(2022, 08, 09, 16, 00, 00) });
            AppointmentList.Add(new Appointment { CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServiceName = "cut", StartDate = new DateTime(2022, 08, 09, 10, 38, 10), EndDate = new DateTime(2022, 08, 09, 13, 38, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "dye", StartDate = new DateTime(2022, 08, 09, 16, 38, 10), EndDate = new DateTime(2022, 09, 08, 17, 20, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "dye", StartDate = new DateTime(2022, 08, 09, 13, 38, 10), EndDate = new DateTime(2022, 09, 08, 15, 20, 10) });
            //Future Days:
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "dye", StartDate = new DateTime(2022, 08, 14, 16, 38, 10), EndDate = new DateTime(2022, 09, 14, 17, 20, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "dye", StartDate = new DateTime(2022, 08, 14, 13, 38, 10), EndDate = new DateTime(2022, 09, 14, 15, 20, 10) });

            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "dye", StartDate = new DateTime(2022, 08, 17, 16, 38, 10), EndDate = new DateTime(2022, 09, 17, 17, 20, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "dye", StartDate = new DateTime(2022, 08, 17, 13, 38, 10), EndDate = new DateTime(2022, 09, 17, 15, 20, 10) });

            AppointmentList.Add(new Appointment { CustomerName = "Adrian Marin", EmployeeName = "Matei Dima", HairServiceName = "cut, wash", StartDate = new DateTime(2022, 08, 20, 16, 30, 00), EndDate = new DateTime(2022, 08, 20, 18, 00, 00) });
            AppointmentList.Add(new Appointment { CustomerName = "Serb David", EmployeeName = "Matei Dima", HairServiceName = "cut, wash", StartDate = new DateTime(2022, 08, 20, 12, 20, 00), EndDate = new DateTime(2022, 08, 20, 14, 00, 00) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "cut, wash", StartDate = new DateTime(2022, 08, 20, 15, 38, 10), EndDate = new DateTime(2022, 08, 20, 17, 20, 10) });
            AppointmentList.Add(new Appointment { CustomerName = "Vlad Apetrica", EmployeeName = "Onofras Rica", HairServiceName = "cut, wash", StartDate = new DateTime(2022, 08, 20, 10, 38, 10), EndDate = new DateTime(2022, 08, 20, 13, 20, 10) });
        }

        public void CreateAppointment(Appointment appointment)
        {
            Console.WriteLine("-> new AppointmentRepository().CreateAppointment()");
            Console.WriteLine("--- start for testing ---");
            Console.WriteLine(appointment.CustomerName);
            Console.WriteLine(appointment.EmployeeName);
            Console.WriteLine(appointment.HairServiceName);
            Console.WriteLine(appointment.StartDate);
            Console.WriteLine(appointment.EndDate);
            AppointmentList.Add(new Appointment { CustomerName = appointment.CustomerName, EmployeeName = appointment.EmployeeName, HairServiceName = appointment.HairServiceName, StartDate = appointment.StartDate, EndDate = appointment.EndDate });
            Console.WriteLine("new list of appointments:");
            foreach (var apps in new AppointmentRepository().GetAllAppointments())
            {
                Console.WriteLine($"'{apps.CustomerName}', '{apps.EmployeeName}', '{apps.HairServiceName}', '{apps.StartDate}', '{apps.EndDate}'.");
            }
            Console.WriteLine("--- end for testing ---");
        }

        // Asta ajuta la Istoricu de Appointments a unui Customer.
        public IEnumerable<Appointment> GetAllAppointmentsByCustomerName(string customerName)
        {
            return AppointmentList.Where(obj => obj.CustomerName == customerName);
        }

        // asta nu stiu daca mai imi trebuie
        public IEnumerable<Appointment> GetInWorkAppointmentsByEmployeeName(string employeeName)
        {
            return AppointmentList
                .Where(obj => obj.StartDate >= DateTime.Now)
                .Where(obj => obj.EmployeeName == employeeName);
        }

        public IEnumerable<Appointment> GetInWorkAppointmentsByEmployeeNameAndDate(string employeeName, DateTime date)
        {
            return AppointmentList
                .Where(obj => obj.StartDate.Date == date.Date)
                .Where(obj => obj.EmployeeName == employeeName);
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return AppointmentList;
        }

        public void UpdateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public void DeleteAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
