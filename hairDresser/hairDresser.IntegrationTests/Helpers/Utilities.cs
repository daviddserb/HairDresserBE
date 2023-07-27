using hairDresser.Domain.Models;
using hairDresser.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.IntegrationTests.Helpers
{
    public static class Utilities
    {
        /// <summary>
        /// Populate the In-Memory Database, where the Tables and Columns are the ones from the Real Database, so the values need to respect the constraints.
        /// </summary>
        /// <param name="db"></param>
        public static void InitializeDbForTests(DataContext db)
        {
            var hairService1 = new HairService
            {
                Id = 1, Name = "wash", Duration = new TimeSpan(00, 07, 00), Price = 49.99m
            };
            var hairService2 = new HairService
            {
                Id = 2, Name = "cut", Duration = new TimeSpan(00, 30, 00), Price = 109.99m
            };
            var hairService3 = new HairService
            {
                Id = 3, Name = "dye", Duration = new TimeSpan(01, 00, 00), Price = 80m
            };
            var hairService4 = new HairService
            {
                Id = 4, Name = "beard", Duration = new TimeSpan(0, 15, 00), Price = 21m
            };
            var hairService5 = new HairService
            {
                Id = 5, Name = "eyebrows", Duration = new TimeSpan(00, 10, 00), Price = 19.99m
            };
            db.HairServices.AddRange(hairService1, hairService2, hairService3, hairService4, hairService5);

            var customer1 = new User
            {
                Id = "80a9c339-2b14-4024-b548-1f782adbda25", UserName = "alfrenieB", Email = "afrenieBalas@gmail.eu", PhoneNumber = "-", Address = "-"
            };
            var customer2 = new User
            {
                Id = "1827e7ba-a97b-4326-ab6b-a847573d86e6", UserName = "mariangigea", Email = "marian$gigea@yahoo.com", PhoneNumber = "+40723143431", Address = "Brasov"
            };
            var employee1 = new User
            {
                Id = "e977b9be-b19c-47bb-bac2-813c3cbd2e97", UserName = "mirceaAnda", Email = "mircea32Anda@gmail.com", PhoneNumber = "+40721243421", Address = "Bucuresti"
            };
            var employee2 = new User
            {
                Id = "fdf45d4c-da75-4a63-aff6-cd9add4d327f", UserName = "alexandra33", Email = "alexandra33@gmail.com", PhoneNumber = "", Address = "Cluj"
            };
            db.Users.AddRange(customer1, customer2, employee1, employee2);

            // Add hair services to employees
            var hairService1_employee2 = new EmployeeHairService
            {
                EmployeeId = employee2.Id, HairServiceId = hairService1.Id
            };
            var hairService2_employee2 = new EmployeeHairService
            {
                EmployeeId = employee2.Id, HairServiceId = hairService2.Id
            };
            db.EmployeesHairServices.AddRange(hairService1_employee2, hairService2_employee2);

            var role1 = new IdentityRole
            {
                Id = "e99e57d1-3805-4f7b-a139-94dc8791a999", Name = "customer"
            };
            var role2 = new IdentityRole
            {
                Id = "2dfc8605-fc95-40a4-8bb0-0ec6e270bdf8", Name = "employee"
            };
            db.Roles.AddRange(role1, role2);
            db.SaveChanges();

            // Add roles to users
            var role1_customer1 = new IdentityUserRole<string>
            {
                UserId = customer1.Id, RoleId = "e99e57d1-3805-4f7b-a139-94dc8791a999"
            };
            var role1_customer2 = new IdentityUserRole<string>
            {
                UserId = customer2.Id, RoleId = "e99e57d1-3805-4f7b-a139-94dc8791a999"
            };
            var role2_employee1 = new IdentityUserRole<string>
            {
                UserId = employee1.Id, RoleId = "2dfc8605-fc95-40a4-8bb0-0ec6e270bdf8"
            };
            var role2_employee2 = new IdentityUserRole<string>
            {
                UserId = employee2.Id, RoleId = "2dfc8605-fc95-40a4-8bb0-0ec6e270bdf8"
            };
            db.UserRoles.AddRange(role1_customer1, role1_customer2, role2_employee1, role2_employee2);

            var workingDay1 = new WorkingDay
            {
                Id = 1, Name = "Monday"
            };
            var workingDay2 = new WorkingDay
            {
                Id = 2, Name = "Tuesday"
            };
            var workingDay3 = new WorkingDay
            {
                Id = 3, Name = "Wednesday"
            };
            var workingDay4 = new WorkingDay
            {
                Id = 4, Name = "Thursday"
            };
            var workingDay5 = new WorkingDay
            {
                Id = 5, Name = "Friday"
            };
            db.WorkingDays.AddRange(workingDay1, workingDay2, workingDay3, workingDay4, workingDay5);

            var workingInterval1 = new WorkingInterval
            {
                Id = 1, WorkingDayId = 4, EmployeeId = "fdf45d4c-da75-4a63-aff6-cd9add4d327f", StartTime = new TimeSpan(09,00,00), EndTime = new TimeSpan(13,00,00)
            };
            var workingInterval2 = new WorkingInterval
            {
                Id = 2, WorkingDayId = 4, EmployeeId = "fdf45d4c-da75-4a63-aff6-cd9add4d327f", StartTime = new TimeSpan(14,00,00), EndTime = new TimeSpan(19,00,00)
            };
            db.WorkingIntervals.AddRange(workingInterval1, workingInterval2);

            var appointment1 = new Appointment
            {
                Id = 1, CustomerId = "1827e7ba-a97b-4326-ab6b-a847573d86e6", EmployeeId = "fdf45d4c-da75-4a63-aff6-cd9add4d327f",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 1, HairServiceId = 1 },
                    new AppointmentHairService { AppointmentId = 1, HairServiceId = 2 }
                },
                StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(3)
            };
            var appointment2 = new Appointment
            {
                Id = 2, CustomerId = "1827e7ba-a97b-4326-ab6b-a847573d86e6", EmployeeId = "e977b9be-b19c-47bb-bac2-813c3cbd2e97",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 2, HairServiceId = 2 },
                    new AppointmentHairService { AppointmentId = 2, HairServiceId = 3 }
                },
                StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddDays(4)
            };
            var appointment3 = new Appointment
            {
                Id = 3, CustomerId = "80a9c339-2b14-4024-b548-1f782adbda25", EmployeeId = "fdf45d4c-da75-4a63-aff6-cd9add4d327f",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 3, HairServiceId = 4 },
                    new AppointmentHairService { AppointmentId = 3, HairServiceId = 5 }
                },
                StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(5)
            };
            db.Appointments.AddRange(appointment1, appointment2, appointment3);

            db.SaveChanges();
        }
    }
}
