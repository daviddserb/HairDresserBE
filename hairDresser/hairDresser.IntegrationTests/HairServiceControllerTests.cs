using hairDresser.Presentation.Dto.HairServiceDtos;
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
    public class HairServiceControllerTests
    {
        private static WebApplicationFactory<Program> _factory = new CustomWebApplicationFactory<Program>();

        [Fact]
        public async Task GetAllHairServices_ShouldReturnOkReponse()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hairservice/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteHairService_ShouldReturnNoContent()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/hairservice/2");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task CreateHairService_ShouldReturnCreatedHairService()
        {
            var newHairService = new HairServicePostPutDto
            {
                Name = "nails",
                DurationInMinutes = 15,
                Price = 20.99m
            };

            var client = _factory.CreateClient();

            var response = await client.PostAsync("api/hairservice",
                new StringContent(JsonConvert.SerializeObject(newHairService), Encoding.UTF8, "application/json"));
            
            var result = await response.Content.ReadAsStringAsync();
            var hairService = JsonConvert.DeserializeObject<HairServiceGetDto>(result);

            Assert.Equal(newHairService.Name, hairService.Name);
            Assert.Equal(newHairService.DurationInMinutes, hairService.Duration.TotalMinutes);
            Assert.Equal(newHairService.Price, hairService.Price);
        }

        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
