using Microsoft.EntityFrameworkCore;

namespace WeatherForecast_BareMinimum.Api.Data;

public class ApplicationDbContext: DbContext
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    public ApplicationDbContext(DbContextOptions options): base(options)
    {
        
    }
}
