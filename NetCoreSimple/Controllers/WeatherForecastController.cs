using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreSimple.Db;
using System;
using System.Linq;

namespace NetCoreSimple.Controllers
{
    [ApiController]
    [EnableCors("OpenCORSPolicy")]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AppDbContext _appDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            var result = await _appDbContext.WeatherForecasts.AddAsync(weatherForecast);
            await _appDbContext.SaveChangesAsync();

             var weatherForecasts = await _appDbContext.WeatherForecasts
                .OrderByDescending(x => x)
                .Take(2)
                .ToListAsync();
            return weatherForecasts;
        }
    }
}