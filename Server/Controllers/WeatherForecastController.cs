using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using APBD_PRO.Shared;
using APBD_PRO.Server.Services;
using APBD_PRO.Server.Models;

namespace APBD_PRO.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IPolygonService _polygonService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IPolygonService polygonService)
    {
        _logger = logger;
        _polygonService = polygonService;
    }

    [HttpGet]
    public async Task<Shared.Aggregates> Get()
    {

        return await _polygonService.GetAggregates("AAPL");
        //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //{
        //    Date = DateTime.Now.AddDays(index),
        //    TemperatureC = Random.Shared.Next(-20, 55),
        //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //})
        //.ToArray();
    }
}

