using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WeatherForecast_BareMinimum.Api.Data;

namespace WeatherForecast_BareMinimum.Api.Controllers;
[ApiController]
[Route("[controller]")]

public class WeatherForecastController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ApplicationDbContext applicationDbContext, ILogger<WeatherForecastController> logger)
    {
        _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        _logger = logger;
    }

    [HttpPost(Name = "SetWeatherForecast")]
    public async Task Post(DateTime date, int temperatureC)
    {
        if (date < DateTime.Now)
        {
            throw new Exception("Date is in the past");
        }

        if (temperatureC < -60 || temperatureC > 60)
        {
            throw new Exception("Temperature is out of range");
        }

        var forecastOfTheDay = _applicationDbContext.WeatherForecasts.SingleOrDefault(w => w.Date == date.Date);
        if (forecastOfTheDay == null)
        {
            forecastOfTheDay = new WeatherForecast
            {
                Id = Guid.NewGuid(),
                TemperatureC = temperatureC,
                Date = date.Date
            };
            _applicationDbContext.Add(forecastOfTheDay);
        }
        else
        {
            forecastOfTheDay.TemperatureC = temperatureC;
            _applicationDbContext.Update(forecastOfTheDay);
        }

        await _applicationDbContext.SaveChangesAsync();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> GetPerWeek(int weekNumber, int year)
    {
        var startOfTheYear = new DateTime(year, 1, 1);
        var dayInTheRequiredWeek = startOfTheYear.AddDays((weekNumber - 1) * 7);
        var dayOfTheWeek = dayInTheRequiredWeek.DayOfWeek;

        var startOfTheWeek = dayInTheRequiredWeek.AddDays(- (int)dayOfTheWeek);
        var endOfTheWeek = startOfTheWeek.AddDays(7);

        return _applicationDbContext.WeatherForecasts.Where(w => w.Date > startOfTheWeek && w.Date < endOfTheWeek);
    }
}
