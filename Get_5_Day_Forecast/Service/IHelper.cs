using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Get_5_Day_Forecast.Model;

namespace Get_5_Day_Forecast.Service
{
    public interface IHelper
    {
        List<AvgDayForecast> CalculateAvgTemps(List<DayForecast> list);
    }
}
