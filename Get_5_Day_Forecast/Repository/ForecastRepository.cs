using Get_5_Day_Forecast.Model;
using System.Collections.Generic;
using System.Linq;

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

        public void RemoveWeatherDataByCity(string city)
        {
            var isCityInTable = new List<AvgDayForecast>();

            if (!string.IsNullOrEmpty(city))
            isCityInTable = _weatherContext.AvgDayForecasts.Where(x => x.City.ToLower() == city.ToLower()).ToList();

            if (isCityInTable != null) _weatherContext.Remove(isCityInTable);
        }

        public List<AvgDayForecast> GetWeatherDataByCity(string city)
        {
            if (!string.IsNullOrEmpty(city)) return _weatherContext.AvgDayForecasts.Where(x => x.City.ToLower() == city.ToLower()).ToList();

            return new List<AvgDayForecast>();
        }
    }
}
