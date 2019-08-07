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
        public decimal maxTemp { get; set; }
        public decimal minTemp { get; set; }
    }
}
