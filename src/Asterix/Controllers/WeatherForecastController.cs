using System.Text.Json;
using AsterixAndObelix.Shared;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AsterixAndObelix.Asterix.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IBus bus;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IBus bus)
    {
        _logger = logger;
        this.bus = bus;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var array = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        var json = JsonSerializer.Serialize(array, new JsonSerializerOptions { WriteIndented = true, });
        MessageRequest messageRequest = new()
        {
            Payload = json,
        };

        await this.bus.Publish(messageRequest);

        return array;
    }
}
