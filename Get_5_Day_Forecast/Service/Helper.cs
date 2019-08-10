using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Get_5_Day_Forecast.Model;

namespace Get_5_Day_Forecast.Service
{
    public class Helper : IHelper
    {
        public List<AvgDayForecastDTO> CalculateAvgTemps(List<DayForecast> list, string city)
        {          
            var avgMaxTemp = 0M;
            var avgMinTemp = 0M;
            var totalMax = 0M;
            var totalMIn = 0M;
            var avgList = new List<AvgDayForecastDTO>();
            var dateString = string.Empty;
            var tempCount = 0;

            List<int> days = list.Select(x => x.date.Day).Distinct().ToList();
            if (days.Count > 5) days.RemoveAt(5);

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

                avgList.Add(new AvgDayForecastDTO
                {
                    City = city,
                    Date = dateString,
                    AvgMaxTemp = Math.Round(avgMaxTemp, 2),
                    AvgMinTemp = Math.Round(avgMinTemp, 2)
                });
            }

            return avgList;
        }

        public bool IsZipCode(string zipCode)
        {
            var _zipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";

            var validZipCode = true;
            if (!Regex.Match(zipCode, _zipRegEx).Success)
            {
                validZipCode = false;
            }
            return validZipCode;
        }

        public bool IsCity(string city)
        {
            var _cityRegEx = @"^[a-zA-Z\- ]+$";

            var validCity = true;
            if (!Regex.Match(city, _cityRegEx).Success)
            {
                validCity = false;
            }
            return validCity;
        }
    }
}
