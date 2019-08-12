using System;

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
