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
        public const string OpenWeatherURL = "http://api.openweathermap.org";
        IHelper _helper;

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

                    var isValidZip = _helper.IsValidZip(input);
                    var isValidCity = _helper.IsValidCity(input);

                    var response = new HttpResponseMessage();

                    if (isValidZip)
                        response = await client.GetAsync($"/data/2.5/forecast?zip={input}&mode=xml&appid=f99e1e3ccd770a8a43db5680342edd6a&units=imperial&days=5");

                    if (isValidCity)
                        response = await client.GetAsync($"/data/2.5/forecast?q={input}&mode=xml&appid=f99e1e3ccd770a8a43db5680342edd6a&units=imperial&days=5");

                    response.EnsureSuccessStatusCode();

                    var weatherXML_Doc = await response.Content.ReadAsStringAsync();
                    var list = _helper.RetrieveDataFromXML(weatherXML_Doc);
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