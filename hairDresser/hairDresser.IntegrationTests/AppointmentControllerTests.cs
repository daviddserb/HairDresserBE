using Microsoft.AspNetCore.Mvc.Testing;
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

            // ??? Cand se intra in functie GetAllAppointments din Controller, mi se returneaza NotFound pt. ca nu se gaseste niciun appointment din db in-memory.
            var response = await client.GetAsync("api/appointment/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
