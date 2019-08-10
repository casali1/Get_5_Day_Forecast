using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Get_5_Day_Forecast.Model;

namespace Get_5_Day_Forecast.Service
{
    public interface IHelper
    {
        List<AvgDayForecastDTO> CalculateAvgTemps(List<DayForecast> list, string input);

        bool IsZipCode(string zipCode);

        bool IsCity(string city);
    }
}
