using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Get_5_Day_Forecast.Model;
using Get_5_Day_Forecast.Service;

namespace Get_5_Day_Forecast.Controllers
{
    [Route("api/Weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        IHelper _helper;
        public const int DataPoints = 9999;
        public const string TimeNodes = "/weatherdata/forecast";
        public const string TempNodes = "temperature";
        public const string OpenWeatherURL = "http://api.openweathermap.org";
        public const string DateNode = "from";
        public const string MaxTempNode = "max";
        public const string MinTempNode = "min";

        public WeatherController(IHelper helper)
        {
            _helper = helper;
        }

        [Route("{input}")]
        public async Task<ActionResult> Input(string input)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(OpenWeatherURL);

                    var isValidZip = _helper.IsZipCode(input);
                    var isCity = _helper.IsCity(input);

                    var response = new HttpResponseMessage();

                    if (isValidZip)
                        response = await client.GetAsync($"/data/2.5/forecast?zip={input}&mode=xml&appid=f99e1e3ccd770a8a43db5680342edd6a&units=imperial&days=5");

                    if (isCity)
                        response = await client.GetAsync($"/data/2.5/forecast?q={input}&mode=xml&appid=f99e1e3ccd770a8a43db5680342edd6a&units=imperial&days=5");

                    response.EnsureSuccessStatusCode();

                    var weatherXML_Doc = await response.Content.ReadAsStringAsync();
                    var doc = new XmlDocument();
                    doc.LoadXml(weatherXML_Doc);
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

                    var avgTempList = _helper.CalculateAvgTemps(list, input);

                    return CreatedAtAction(nameof(input), avgTempList);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}