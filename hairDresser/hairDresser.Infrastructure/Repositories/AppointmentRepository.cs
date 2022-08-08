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
            AppointmentList.Add(new Appointment("Adrian Marin", "Matei Dima", "cut, wash", new DateTime(2022, 08, 02, 16, 40, 10), new DateTime(2022, 08, 02, 17, 00, 00)));
            AppointmentList.Add(new Appointment("Serb David", "Matei Dima", "cut", new DateTime(2022, 08, 02, 11, 38, 10), new DateTime(2022, 08, 02, 13, 38, 10)));
            AppointmentList.Add(new Appointment("Vlad Apetrica", "Onofras Rica", "dye", new DateTime(2022, 08, 02, 10, 40, 10), new DateTime(2022, 08, 08, 11, 20, 10)));

            //In Work (Not Finished) Appointments:
            AppointmentList.Add(new Appointment("Adrian Marin", "Matei Dima", "cut, wash", new DateTime(2022, 08, 08, 14, 40, 10), new DateTime(2022, 08, 08, 16, 00, 00)));
            AppointmentList.Add(new Appointment("Serb David", "Matei Dima", "cut", new DateTime(2022, 08, 08, 10, 38, 10), new DateTime(2022, 08, 08, 13, 38, 10)));
            AppointmentList.Add(new Appointment("Vlad Apetrica", "Onofras Rica", "dye", new DateTime(2022, 08, 08, 16, 38, 10), new DateTime(2022, 08, 08, 17, 20, 10)));
            AppointmentList.Add(new Appointment("Vlad Apetrica", "Onofras Rica", "dye", new DateTime(2022, 08, 08, 13, 38, 10), new DateTime(2022, 08, 08, 15, 20, 10)));

            AppointmentList.Add(new Appointment("Adrian Marin", "Matei Dima", "cut, wash", new DateTime(2022, 08, 20, 16, 30, 00), new DateTime(2022, 08, 20, 18, 00, 00)));
            AppointmentList.Add(new Appointment("Serb David", "Matei Dima", "cut", new DateTime(2022, 08, 20, 12, 20, 00), new DateTime(2022, 08, 20, 14, 00, 00)));
            AppointmentList.Add(new Appointment("Vlad Apetrica", "Onofras Rica", "dye", new DateTime(2022, 08, 20, 15, 38, 10), new DateTime(2022, 08, 20, 17, 20, 10)));
            AppointmentList.Add(new Appointment("Vlad Apetrica", "Onofras Rica", "dye", new DateTime(2022, 08, 20, 10, 38, 10), new DateTime(2022, 08, 20, 13, 20, 10)));
        }

        public void CreateAppointment(Appointment appointment)
        {
            // STEPS:
            // 1. Let customer pick what hair services he wants.
            // 2. Based on what he chosed, give him all the Employee's names that each of them can do all of his hair services.
            // 3. Let customer look through each Employee's available dates and pick one.
            throw new NotImplementedException();
        }

        // Asta ajuta la Istoricu de Appointments a unui Customer.
        public IEnumerable<Appointment> GetAllAppointmentsByCustomerName(string customerName)
        {
            return AppointmentList.Where(obj => obj.CustomerName == customerName);
        }

        public IEnumerable<Appointment> GetInWorkAppointmentsByEmployeeName(string employeeName)
        {
            return AppointmentList
                .Where(obj => obj.StartDate >= DateTime.Now)
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
