using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Get_5_Day_Forecast.Model
{
    public class DayForecast
    {
        public int Index { get; set; }
        public DateTime Date { get; set; }
        public decimal MaxTemp { get; set; }
        public decimal MinTemp { get; set; }
    }
}
