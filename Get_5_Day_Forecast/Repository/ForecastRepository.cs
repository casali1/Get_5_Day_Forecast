using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Get_5_Day_Forecast.Model;

namespace Get_5_Day_Forecast.Repository
{
    public class ForecastRepository : IForecastRepository
    {
        readonly WeatherContext _weatherContext;

        public ForecastRepository(WeatherContext weatherContext)
        {
            _weatherContext = weatherContext;
        }

        public void SaveRequestedData(AvgDayForecast avgDayForecast)
        {
            _weatherContext.AvgDayForecasts.Add(avgDayForecast);
            _weatherContext.SaveChanges();
        }
    }
}
