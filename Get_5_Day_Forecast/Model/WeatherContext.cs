using Microsoft.EntityFrameworkCore;

namespace Get_5_Day_Forecast.Model
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options): base(options)
        {
            Database.Migrate();
        }

        public DbSet<AvgDayForecast> AvgDayForecasts { get; set; }
    }
}
