using hairDresser.Domain.Models;
using hairDresser.Presentation.Dto.AppointmentDtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.IntegrationTests
{
    public class AppointmentControllerTests
    {
        private static WebApplicationFactory<Program> _factory = new CustomWebApplicationFactory<Program>();

        [Fact]
        public async Task GetAllAppointments_ShouldReturnOkReponse()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/appointment/all?PageNumber=1&PageSize=1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAppointment_ShouldReturnNoContent()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/appointment/1");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task CreateAppointment_ShouldReturnCreatedAppointment()
        {
            var newAppointment = new AppointmentPostDto
            {
                CustomerId = 1,
                EmployeeId = 2,
                StartDate = DateTime.Now.AddHours(4),
                EndDate = DateTime.Now.AddHours(6),

                // List este reference type si atunci trebuie folosit new.
                HairServicesIds = new List<int> { 3, 4 }
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("api/appointment",
                new StringContent(JsonConvert.SerializeObject(newAppointment), Encoding.UTF8, "application/json"));

            var result = await response.Content.ReadAsStringAsync();
            var appointment = JsonConvert.DeserializeObject<AppointmentGetDto>(result);

            Assert.Equal(newAppointment.StartDate, appointment.StartDate);
        }

        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
