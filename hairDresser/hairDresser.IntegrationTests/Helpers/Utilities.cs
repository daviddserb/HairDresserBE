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

            var customer1 = new Customer
            {
                Id = 1,
                Name = "Grigore",
                Username = "grigore",
                Password = "parolagrea123",
                Email = "grigore@yahoo.com",
                Phone = "+40765345123",
                Address = "Timisoara"

            };
            var customer2 = new Customer
            {
                Id = 2,
                Name = "Andrei",
                Username = "andrei",
                Password = "321parolagrea",
                Email = "andrei@yahoo.com",
                Phone = "+40764376123",
                Address = "Arad"
            };
            var customer3 = new Customer
            {
                Id = 3,
                Name = "Marcel",
                Username = "marcel",
                Password = "321parolagrea123",
                Email = "marcel@yahoo.com",
                Phone = "+40761176432",
                Address = "Bucuresti"
            };
            db.Customers.AddRange(customer1, customer2, customer3);

            var employee1 = new Employee
            {
                Id = 1,
                Name = "Matei",
                // BEFORE:
                //EmployeeHairServices = new List<EmployeeHairService>()
                //{
                //    new EmployeeHairService { EmployeeId = "11223344-5566-7788-99AA-BBCCDDEEFF00", HairServiceId = 1 },
                //    new EmployeeHairService { EmployeeId = "11223344-5566-7788-99AA-BBCCDDEEFF00", HairServiceId = 2 }
                //}
            };
            var employee2 = new Employee
            {
                Id = 2,
                Name = "Onofras",
                // BEFORE:
                //EmployeeHairServices = new List<EmployeeHairService>()
                //{
                //    new EmployeeHairService { EmployeeId = "11223344-5566-7788-99AB-BBCCDDEEFF00", HairServiceId = 2 },
                //    new EmployeeHairService { EmployeeId = "11223344-5566-7788-99AB-BBCCDDEEFF00", HairServiceId = 3 }
                //}
            };
            var employee3 = new Employee
            {
                Id = 3,
                Name = "Andreea",
                // BEFORE:
                //EmployeeHairServices = new List<EmployeeHairService>()
                //{
                //    new EmployeeHairService { EmployeeId = "11223344-5566-7788-99AB-BBCCDDEEFF00", HairServiceId = 4 },
                //    new EmployeeHairService { EmployeeId = "11223344-5566-7788-99AB-BBCCDDEEFF00", HairServiceId = 5 }
                //}
            };
            db.Employees.AddRange(employee1, employee2, employee3);

            var appointment1 = new Appointment
            {
                Id = 1,
                CustomerId = "21223344-5566-7788-99AB-BBCCDDEEFF00",
                EmployeeId = "11223344-5566-7788-99AA-BBCCDDEEFF00",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 1, HairServiceId = 1 },
                    new AppointmentHairService { AppointmentId = 1, HairServiceId = 2 }
                },
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            var appointment2 = new Appointment
            {
                Id = 2,
                CustomerId = "31223344-5566-7788-99AB-BBCCDDEEFF00",
                EmployeeId = "11223344-5566-7788-99AB-BBCCDDEEFF00",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 2, HairServiceId = 2 },
                    new AppointmentHairService { AppointmentId = 2, HairServiceId = 3 }
                },
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            var appointment3 = new Appointment
            {
                Id = 3,
                CustomerId = "41223344-5566-7788-99AB-BBCCDDEEFF00",
                EmployeeId = "11223344-5566-7788-99AB-BBCCDDEEFF00",
                AppointmentHairServices = new List<AppointmentHairService>()
                {
                    new AppointmentHairService { AppointmentId = 3, HairServiceId = 4 },
                    new AppointmentHairService { AppointmentId = 3, HairServiceId = 5 }
                },
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            db.Appointments.AddRange(appointment1, appointment2, appointment3);

            db.SaveChanges();
        }
    }
}
