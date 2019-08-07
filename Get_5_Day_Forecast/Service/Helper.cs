using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Get_5_Day_Forecast.Model;

namespace Get_5_Day_Forecast.Service
{
    public class Helper : IHelper
    {
        public List<AvgDayForecast> CalculateAvgTemps(List<DayForecast> list)
        {
            List<int> days = list.Select(x => x.date.Day).Distinct().ToList();
            if (days.Count > 5) days.RemoveAt(0);

            var avgMaxTemp = 0M;
            var avgMinTemp = 0M;
            var totalMax = 0M;
            var totalMIn = 0M;
            var avgList = new List<AvgDayForecast>();
            var dateString = "";

            var tempCount = 0;
            foreach (var day in days)
            {
                var tempList = list.Where(x => x.date.Day == day);

                foreach (var record in tempList)
                {
                    totalMax = totalMax + record.maxTemp;
                    totalMIn = totalMIn + record.minTemp;
                    tempCount++;
                    dateString = record.date.Date.ToString();
                }

                avgMaxTemp = totalMax / tempCount;
                avgMinTemp = totalMIn / tempCount;

                avgList.Add(new AvgDayForecast
                {
                    date = dateString,
                    avgMaxTemp = avgMaxTemp,
                    avgMinTemp = avgMinTemp
                });
            }

            return avgList;
        }
    }
}
