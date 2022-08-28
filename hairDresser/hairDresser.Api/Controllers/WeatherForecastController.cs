using Microsoft.AspNetCore.Mvc;

namespace hairDresser.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // ??? De ce nu ma lasa sa pun si aceasta metoda? Ma gandesc ca ii pt. ca nu fac diferenta intre asta si cea cu GetById()?
        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> GetAll()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        // Ce adaug eu mai departe:
        [HttpGet(Name = "GetWeatherForecast")]
        public WeatherForecast GetById([FromQuery] int index)
        {
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPost]
        public WeatherForecast Post([FromBody] WeatherForecast body)
        {
            return body;
            // in body vei avea toate rezultatele ca input din API si de aici dai call la MediatR pe care apelezi Query/Command (ce ai si in aplicatia de Consola, dar fara Console.ReadLine()-uri).
        }
    }
}