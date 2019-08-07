using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Get_5_Day_Forecast.Model;

namespace Get_5_Day_Forecast.Controllers
{
    [Route("api/Weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [Route("{city}")]
        public async Task<ActionResult> City(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    //var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=f99e1e3ccd770a8a43db5680342edd6a&units=imperial");
                    var response = await client.GetAsync($"/data/2.5/forecast?q={city}&mode=xml&appid=f99e1e3ccd770a8a43db5680342edd6a&units=imperial&days=5");

                    response.EnsureSuccessStatusCode();

                    var weatherXML_Doc = await response.Content.ReadAsStringAsync();

                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(weatherXML_Doc);

                    XmlElement root = doc.DocumentElement;

                    XmlNodeList nodes = root.SelectNodes("/weatherdata/forecast"); // You can also use XPath here

                    var list = new List<DayForecast>();
                    foreach (XmlNode node in nodes)
                    {
                        for (int i = 0; i <= 9999; i++)
                        {
                            if (node.ChildNodes[i].HasChildNodes)
                            {

                                for (int j = 0; j <= 9999; j++)
                                {
                                    var childOfAChild_Name = node.ChildNodes[i].ChildNodes[j].Name;

                                    if(childOfAChild_Name == "temperature")
                                    {


                                    }

                                }

                                list.Add(new DayForecast
                                {
                                    index = i,
                                    date = Convert.ToDateTime(node.ChildNodes[i].Attributes["from"].Value)
                                });
                            }
                            else
                                break;
                        }
                    }

                    nodes = root.SelectNodes("/weatherdata/forecast/time/temperature");

                    foreach (XmlNode node in nodes)
                    {
                        for (int i = 0; i <= list.Count; i++)
                        {
                            var nodeDiscovered = node.ChildNodes[i];

                            if (nodeDiscovered != null)
                            {
                                list.Add(new DayForecast
                                {
                                    maxTemp = Convert.ToInt32(node.ChildNodes[i].Attributes["max"].Value),
                                    minTemp = Convert.ToInt32(node.ChildNodes[i].Attributes["min"].Value)
                                });
                            }
                            else
                                break;
                        }

                    }





                        //var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);

                        //var temp = rawWeather.Main.Temp;
                        //var sum = string.Join(",", rawWeather.Weather.Select(x => x.Main));
                        //var c = rawWeather.Name;


                        //return View(new Data
                        //{
                        //    Temp = rawWeather.Main.Temp,
                        //    Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        //    City = rawWeather.Name
                        //});
                        return NotFound("Role not found");
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new Exception();
                    //return View($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }
    }
}