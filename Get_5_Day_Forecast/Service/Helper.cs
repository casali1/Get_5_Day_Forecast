using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Get_5_Day_Forecast.Model;
using Get_5_Day_Forecast.Repository;

namespace Get_5_Day_Forecast.Service
{
    public class Helper : IHelper
    {
        public const int DataPoints = 9999;
        public const string TimeNodes = "/weatherdata/forecast";
        public const string TempNodes = "temperature";
        public const string DateNode = "from";
        public const string MaxTempNode = "max";
        public const string MinTempNode = "min";

        IForecastRepository _forecastRepository;

        public Helper(IForecastRepository forecastRepository)
        {
            _forecastRepository = forecastRepository;
        }

        public bool IsValidZip(string zipCode)
        {
            var _zipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";

            var validZipCode = true;
            if (!Regex.Match(zipCode, _zipRegEx).Success)
            {
                validZipCode = false;
            }
            return validZipCode;
        }

        public bool IsValidCity(string city)
        {
            var _cityRegEx = @"^[a-zA-Z\- ]+$";

            var validCity = true;
            if (!Regex.Match(city, _cityRegEx).Success)
            {
                validCity = false;
            }
            return validCity;
        }

        public List<DayForecast> RetrieveDataFromXML(string weatherData)
        {
            var doc = new XmlDocument();
            doc.LoadXml(weatherData);
            var root = doc.DocumentElement;
            var nodes = root.SelectNodes(TimeNodes); // You can also use XPath here
            var list = new List<DayForecast>();

            //Retrieve data from XML.
            foreach (XmlNode node in nodes)
            {
                for (int i = 0; i <= DataPoints; i++)
                {
                    var hasNode = node.ChildNodes[i];
                    if (hasNode != null)
                    {
                        var maxTemp = 0M;
                        var minTemp = 0M;
                        for (int j = 0; j <= DataPoints; j++)
                        {
                            var hasChildNode = node.ChildNodes[i].ChildNodes[j];
                            if (hasChildNode != null)
                            {
                                var childOfAChild_Name = node.ChildNodes[i].ChildNodes[j].Name;

                                if (childOfAChild_Name == TempNodes)
                                {
                                    maxTemp = Convert.ToDecimal(node.ChildNodes[i].ChildNodes[j].Attributes[MaxTempNode].Value);
                                    minTemp = Convert.ToDecimal(node.ChildNodes[i].ChildNodes[j].Attributes[MinTempNode].Value);
                                    break;
                                }
                            }
                            else
                                break;
                        }

                        list.Add(new DayForecast
                        {
                            Index = i,
                            Date = Convert.ToDateTime(node.ChildNodes[i].Attributes[DateNode].Value),
                            MaxTemp = maxTemp,
                            MinTemp = minTemp
                        });
                    }
                    else
                        break;
                }
            }

            return list;
        }

        public List<AvgDayForecastDTO> CalculateAvgTemps(List<DayForecast> list, string city)
        {          
            var avgMaxTemp = 0M;
            var avgMinTemp = 0M;
            var totalMax = 0M;
            var totalMIn = 0M;
            var avgList = new List<AvgDayForecastDTO>();
            var dateString = string.Empty;
            var tempCount = 0;

            List<int> days = list.Select(x => x.Date.Day).Distinct().ToList();
            if (days.Count > 5) days.RemoveAt(5);

            foreach (var day in days)
            {
                var tempList = list.Where(x => x.Date.Day == day);

                foreach (var record in tempList)
                {
                    totalMax = totalMax + record.MaxTemp;
                    totalMIn = totalMIn + record.MinTemp;
                    tempCount++;
                    dateString = record.Date.Date.ToString();
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

                //Store requested to Database.
                _forecastRepository.SaveRequestedData(new AvgDayForecast
                {
                    ForecastId = Guid.NewGuid(),
                    City = city,
                    Date = dateString,
                    AvgMaxTemp = Math.Round(avgMaxTemp, 2),
                    AvgMinTemp = Math.Round(avgMinTemp, 2)
                });
            }

            return avgList;
        }
    }
}
