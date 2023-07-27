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
        public async Task CreateAppointment_ShouldReturnCreatedAppointment()
        {
            // Appointment data input must be valid in order for the Test to Succed.
            var newAppointment = new AppointmentPostDto
            {
                CustomerId = "80a9c339-2b14-4024-b548-1f782adbda25",
                EmployeeId = "fdf45d4c-da75-4a63-aff6-cd9add4d327f",
                //List is Reference Type => need to use new keyword.
                HairServicesIds = new List<int> { 1, 2 },
                Price = 159.98f,
                StartDate = new DateTime(2023, 08, 24, 09, 00, 00),
                EndDate = new DateTime(2023, 08, 24, 09, 37, 00)
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("api/appointment",
                new StringContent(JsonConvert.SerializeObject(newAppointment), Encoding.UTF8, "application/json"));

            var result = await response.Content.ReadAsStringAsync();
            var appointment = JsonConvert.DeserializeObject<AppointmentGetDto>(result);

            Assert.Equal(newAppointment.StartDate, appointment.StartDate);
        }

        [Fact]
        public async Task DeleteAppointment_ShouldReturnNoContent()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/appointment/1");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
