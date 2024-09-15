using Microsoft.AspNetCore.Mvc;
using SubjectTracker.Models;
using SubjectTracker.StorageBrokers;

namespace SubjectTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly istorageBroker istorageBroker;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, istorageBroker storageBroker)
        {
            this.istorageBroker = storageBroker;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task< IEnumerable<WeatherForecast>> Get()
        {
          Subject Subject = new Subject();
            Subject.Name = "SubjectTracker";
            Subject.Id = Guid.NewGuid();
            await istorageBroker.InsertSubjectAsync(Subject); 

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
