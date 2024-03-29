﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{input}")]
        public async Task<ActionResult> GetWeatherData(string input)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(OpenWeatherURL);

                    var isValidZip = _helper.IsValidZip(input); //Validate the Zip Code if entered.
                    var isValidCity = _helper.IsValidCity(input); //Validate the City if entered.

                    var response = new HttpResponseMessage();

                    //Consuming the end points of the OpenWeather.
                    if (isValidZip)
                        response = await client.GetAsync($"/data/2.5/forecast?zip={input}&mode=xml&appid=f99e1e3ccd770a8a43db5680342edd6a&units=imperial&days=5");

                    if (isValidCity)
                        response = await client.GetAsync($"/data/2.5/forecast?q={input}&mode=xml&appid=f99e1e3ccd770a8a43db5680342edd6a&units=imperial&days=5");

                    response.EnsureSuccessStatusCode();

                    var weatherXML_Doc = await response.Content.ReadAsStringAsync();
                    var list = _helper.RetrieveDataFromXML(weatherXML_Doc);
                    var avgTempList = _helper.CalculateAvgTemps(list, input);

                    //return CreatedAtAction(nameof(input), avgTempList); <-------Use for POST.
                    return Ok(avgTempList);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}