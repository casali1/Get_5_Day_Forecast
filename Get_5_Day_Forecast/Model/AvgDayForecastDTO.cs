

namespace Get_5_Day_Forecast.Model
{
    public class AvgDayForecastDTO
    {
        public string City { get; set; }
        public string Date { get; set; }
        public decimal AvgMaxTemp { get; set; }
        public decimal AvgMinTemp { get; set; }
    }
}
