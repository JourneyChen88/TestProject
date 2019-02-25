using BlazorDemo.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorDemo.Api.Controllers
{
    [Route("api/sampledata")]
    [ApiController]
    public class SampleDataController
        : Controller
    {
        private static string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("WeatherForecasts/{dateTime}")]
        public IEnumerable<WeatherForecast> WeatherForecasts(DateTime dateTime)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = dateTime.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public string Get()
        {
            return "running";
        }
    }
}