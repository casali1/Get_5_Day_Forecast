using Get_5_Day_Forecast.Model;

namespace Get_5_Day_Forecast.Repository
{
    public interface IForecastRepository
    {
        void SaveRequestedData(AvgDayForecast avgDayForecast);
    }
}
