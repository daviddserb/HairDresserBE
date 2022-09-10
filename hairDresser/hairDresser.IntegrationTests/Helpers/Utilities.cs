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
                CustomerId = 1,
                EmployeeId = 1,

                //AppointmentHairServices = new AppointmentHairService() {
                //    HairServiceId = 1;
                //},

                //AppointmentHairServices = new AppointmentHairService
                //{
                //    HairServiceId = 1,
                //},

                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            var appointment2 = new Appointment
            {
                CustomerId = 2,
                EmployeeId = 2,
                //AppointmentHairServices =
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            var appointment3 = new Appointment
            {
                CustomerId = 3,
                EmployeeId = 3,
                //AppointmentHairServices = 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            //db.Appointments.AddRange(appointment1, appointment2, appointment3);

            var hairServices1 = new HairService
            {
                Name = "haircut",
                Duration = new TimeSpan(01, 30, 00),
                Price = 100
            };
            var hairServices2 = new HairService
            {
                Name = "wash",
                Duration = new TimeSpan(00, 30, 00),
                Price = 50
            };
            var hairServices3 = new HairService
            {
                Name = "dye",
                Duration = new TimeSpan(02, 00, 00),
                Price = 200
            };
            db.HairServices.AddRange(hairServices1, hairServices2, hairServices3);

            db.SaveChanges();
        }
    }
}
