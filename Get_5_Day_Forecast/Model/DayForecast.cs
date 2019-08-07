using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Get_5_Day_Forecast.Model
{
    public class DayForecast
    {
        public int index { get; set; }
        public DateTime date { get; set; }
        public int maxTemp { get; set; }
        public int minTemp { get; set; }
    }
}
