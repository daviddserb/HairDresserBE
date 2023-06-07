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
            var newAppointment = new AppointmentPostDto
            {
                CustomerId = "a27325c9-7b54-4058-a6c3-3e076edb3a77",
                EmployeeId = "e977b9be-b19c-47bb-bac2-813c3cbd2e97",
                StartDate = DateTime.Now.AddHours(4),
                EndDate = DateTime.Now.AddHours(6),
                HairServicesIds = new List<int> { 3, 4 } //List is reference type => we need to use the new keyword.
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
