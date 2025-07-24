using hairDresser.Presentation.Dto.AppointmentDtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace hairDresser.IntegrationTests
{
    public class AppointmentControllerTests
    {
        private static WebApplicationFactory<Program> _factory = new CustomWebApplicationFactory<Program>();

        [Fact]
        public async Task GetAllAppointments_ShouldReturnOkReponse()
        {
            //Arrange:
            var client = _factory.CreateClient();
            //Act:
            var response = await client.GetAsync("api/appointment/all?PageNumber=1&PageSize=1"); // hard-coded the parameter values
            //Assert:
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateAppointment_ShouldReturnCreatedAppointment()
        {
            var client = _factory.CreateClient();

            var currentDate = DateTime.Now;
            while (currentDate.Hour != 9) currentDate = currentDate.AddHours(1);
            while (currentDate.DayOfWeek != DayOfWeek.Thursday) currentDate = currentDate.AddDays(1);

            // Appointment data input must be valid in order for the test to succeed.
            var newAppointment = new AppointmentPostDto
            {
                CustomerId = "80a9c339-2b14-4024-b548-1f782adbda25",
                EmployeeId = "fdf45d4c-da75-4a63-aff6-cd9add4d327f",
                HairServicesIds = new List<int> { 1, 2 },
                Price = 159.98f,
                StartDate = currentDate,
                EndDate = currentDate.AddMinutes(37)
            };

            var response = await client.PostAsync("api/appointment", new StringContent(JsonConvert.SerializeObject(newAppointment), Encoding.UTF8, "application/json"));

            var result = await response.Content.ReadAsStringAsync();
            var appointment = JsonConvert.DeserializeObject<AppointmentGetDto>(result);

            Assert.Equal(newAppointment.StartDate, appointment.StartDate);
        }

        [Fact]
        public async Task DeleteAppointment_ShouldReturnNoContent()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/appointment/1827e7ba-a97b-4326-ab6b-a847573d86e6/1");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
