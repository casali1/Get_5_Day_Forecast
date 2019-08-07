using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Get_5_Day_Forecast.Model
{
    public class AvgDayForecast
    {
        public DateTime date { get; set; }
        public int avgMaxTemp { get; set; }
        public int avgMinTemp { get; set; }
    }
}
