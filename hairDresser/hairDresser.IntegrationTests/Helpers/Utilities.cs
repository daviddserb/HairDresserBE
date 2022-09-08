using hairDresser.Domain.Models;
using hairDresser.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(DataContext db)
        {
            var appointment1 = new Appointment
            {
                Id = 1,
                CustomerId = 1,
                EmployeeId = 1,
                // !!! sa vad cum o sa salvez si hairservices (asta vad eu, nu-i eroare fara ele cred)
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };
            var appointment2 = new Appointment
            {
                Id = 2,
                CustomerId = 2,
                EmployeeId = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };
            var appointment3 = new Appointment
            {
                Id = 3,
                CustomerId = 3,
                EmployeeId = 3,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };
            db.Appointments.AddRange(appointment1, appointment2, appointment3);
            db.SaveChanges();

            var hairServices1 = new HairService
            {
                Id = 1,
                Name = "haircut",
                Duration = new TimeSpan(01, 30, 00),
                Price = 100
            };
            var hairServices2 = new HairService
            {
                Id = 2,
                Name = "wash",
                Duration = new TimeSpan(00, 30, 00),
                Price = 50
            };
            var hairServices3 = new HairService
            {
                Id = 3,
                Name = "dye",
                Duration = new TimeSpan(02, 00, 00),
                Price = 200
            };
            db.HairServices.AddRange(hairServices1, hairServices2, hairServices3);
            db.SaveChanges();
        }
    }
}
