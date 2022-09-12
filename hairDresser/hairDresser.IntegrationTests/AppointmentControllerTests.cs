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

            //var response = await client.GetAsync("api/appointment/all"); //before
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

        //[Fact]
        //public async Task CreateAppointment_ShouldReturnCreatedAppointment()
        //{
        //    var newAppointment = new AppointmentPostDto
        //    {
        //        CustomerId = 1,
        //        EmployeeId = 2,
        //        StartDate = DateTime.Now.AddHours(4),
        //        EndDate = DateTime.Now.AddHours(6),

        //        // ???
        //        // De ce am eroare cand salvez niste numere intr-o lista de int-uri?
        //        // Daca nu pun proprietatea HairServicesIds si nu salvez nimic in ea, nu mi se intra in metoda din Controller, si m-am gandit ca ii din cauza asta (nu stiu sigur).
        //        //M-am gandit sa nu fie de la faptul ca le salvez sub forma {1, 2} ci nu [1, 2], pt. ca pe API, cand le salvez, numerele sunt in [], dar aici nu merge cu [].
        //        //HairServicesIds = { 1, 2, 3 }
        //        HairServicesIds = { 3 }
        //    };

        //    var client = _factory.CreateClient();

        //    // ???
        //    // Aici nu mi se intra in AppointmentController -> CreateAppointment(), si m-am gandit ca poate ii pt. ca nu am salvat si HairServicesIds in newAppointment, cu toate ca nu
        //    //cred ca asta este motivul, dar alta idee nu am...
        //    var response = await client.PostAsync("api/appointment",
        //        new StringContent(JsonConvert.SerializeObject(newAppointment), Encoding.UTF8, "application/json"));

        //    var result = await response.Content.ReadAsStringAsync();
        //    var appointment = JsonConvert.DeserializeObject<AppointmentGetDto>(result);

        //    Assert.Equal(newAppointment.CustomerId, appointment.CustomerId);
        //    Assert.Equal(newAppointment.EmployeeId, appointment.EmployeeId);
        //    Assert.Equal(newAppointment.StartDate, appointment.StartDate);
        //}

        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
