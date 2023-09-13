namespace WeatherForecast_BareMinimum.Api;

public class WeatherForecast
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary
    {
        get
        {
            switch(TemperatureC)
            {
                case int temperature when (temperature < -10):
                    return "Freezing";
                case int temperature when (temperature < 0):
                    return "Bracing";
                case int temperature when (temperature < 10):
                    return "Chilly";
                case int temperature when (temperature < 15):
                    return "Cool";
                case int temperature when (temperature < 20):
                    return "Mild";
                case int temperature when (temperature < 25):
                    return "Warm";
                case int temperature when (temperature < 30):
                    return "Balmy";
                case int temperature when (temperature < 35):
                    return "Hot";
                case int temperature when (temperature >= 35):
                    return "Sweltering";
                default:
                    return string.Empty;
            }
        }
    }
}
