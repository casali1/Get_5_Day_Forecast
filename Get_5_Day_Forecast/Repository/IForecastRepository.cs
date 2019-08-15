using Get_5_Day_Forecast.Model;
using System.Collections.Generic;

namespace Get_5_Day_Forecast.Repository
{
    public interface IForecastRepository
    {
        void SaveRequestedData(AvgDayForecast avgDayForecast);

        void RemoveWeatherDataByCity(string city);

        List<AvgDayForecast> GetWeatherDataByCity(string city);
    }
}
