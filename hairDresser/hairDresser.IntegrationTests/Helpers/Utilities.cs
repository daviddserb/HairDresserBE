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
            var hairServices1 = new HairService
            {
                Id = 1,
                Name = "wash",
                Duration = new TimeSpan(00, 30, 00),
                Price = 87
            };
            var hairServices2 = new HairService
            {
                Id = 2,
                Name = "cut",
                Duration = new TimeSpan(01, 00, 00),
                Price = 100
            };
            var hairServices3 = new HairService
            {
                Id = 3,
                Name = "dye",
                Duration = new TimeSpan(01, 30, 00),
                Price = 150
            };
            var hairServices4 = new HairService
            {
                Id = 4,
                Name = "beard",
                Duration = new TimeSpan(0, 20, 00),
                Price = 20
            };
            var hairServices5 = new HairService
            {
                Id = 5,
                Name = "eyebrows",
                Duration = new TimeSpan(00, 10, 00),
                Price = 15
            };
            db.HairServices.AddRange(hairServices1, hairServices2, hairServices3, hairServices4, hairServices5);

            var user1 = new User
            {
                Id = "a27325c9-7b54-4058-a6c3-3e076edb3a77", // customer id
                UserName = "cristi",
                Email = "cristi@yahoo.com",
                PhoneNumber = "+40 742 237 921",
                Address = "Timisoara",
            };
            var user2 = new User
            {
                Id = "e977b9be-b19c-47bb-bac2-813c3cbd2e97", // employee id
                UserName = "mircea",
                Email = "mircea@yahoo.com",
                PhoneNumber = "+40 322 237 921",
                Address = "Maramures",
            };
            var user3 = new User
            {
                Id = "1827e7ba-a97b-4326-ab6b-a847573d86e6", // customer id
                UserName = "mihai",
                Email = "mihai@yahoo.com",
                PhoneNumber = "+40 322 237 123",
                Address = "Targoviste",
            };
            var user4 = new User
            {
                Id = "fdf45d4c-da75-4a63-aff6-cd9add4d327f", // employee id
                UserName = "alex",
                Email = "alex@gmail.com",
                PhoneNumber = "+40 123 237 921",
                Address = "Constanta",
            };
            db.Users.AddRange(user1, user2, user3, user4);

            var appointment1 = new Appointment
            {
                Id = 1,
                CustomerId = "1827e7ba-a97b-4326-ab6b-a847573d86e6",
                EmployeeId = "fdf45d4c-da75-4a63-aff6-cd9add4d327f",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 1, HairServiceId = 1 },
                    new AppointmentHairService { AppointmentId = 1, HairServiceId = 2 }
                },
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(2)
            };
            var appointment2 = new Appointment
            {
                Id = 2,
                CustomerId = "1827e7ba-a97b-4326-ab6b-a847573d86e6",
                EmployeeId = "e977b9be-b19c-47bb-bac2-813c3cbd2e97",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 2, HairServiceId = 2 },
                    new AppointmentHairService { AppointmentId = 2, HairServiceId = 3 }
                },
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(3)
            };
            var appointment3 = new Appointment
            {
                Id = 3,
                CustomerId = "a27325c9-7b54-4058-a6c3-3e076edb3a77",
                EmployeeId = "fdf45d4c-da75-4a63-aff6-cd9add4d327f",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 3, HairServiceId = 4 },
                    new AppointmentHairService { AppointmentId = 3, HairServiceId = 5 }
                },
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(4)
            };
            db.Appointments.AddRange(appointment1, appointment2, appointment3);

            db.SaveChanges();
        }
    }
}
