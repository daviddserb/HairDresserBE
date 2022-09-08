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
        private static WebApplicationFactory<Program> _factory;
    
        public static void ClassInit()
        {
            _factory = new CustomWebApplicationFactory<Program>();
        }

        [Fact]
        public async Task GetAllAppointmnets_ShouldReturnOkReponse()
        {
            // ??? am eroare aici (Object reference not set to an instance of an object)
            var client = _factory.CreateClient();

            // ! s-ar putea sa nu fie buna ruta din GetAsync(), adica api/...
            var response = await client.GetAsync("api/appointment/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public static void ClassCleanup()
        {
            _factory.Dispose();
        }


    }
}
