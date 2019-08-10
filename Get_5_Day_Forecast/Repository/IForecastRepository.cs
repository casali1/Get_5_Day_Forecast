using Get_5_Day_Forecast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Get_5_Day_Forecast.Repository
{
    public interface IForecastRepository
    {
        void SaveRequestedData(AvgDayForecast avgDayForecast);
    }
}
