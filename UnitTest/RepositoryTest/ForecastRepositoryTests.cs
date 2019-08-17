using Get_5_Day_Forecast.Model;
using Get_5_Day_Forecast.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest.RepositoryTest
{
    [TestClass]
    public class ForecastRepositoryTests
    {
        readonly WeatherContext _weatherContext;

        public ForecastRepositoryTests(WeatherContext weatherContext)
        {
            _weatherContext = weatherContext;
        }


        public void DeleteRowsOfDataByCity()
        {

            //var forecastRepository = new ForecastRepository(_weatherContext);

            //var list = forecastRepository.GetWeatherDataByCity("DENVER");

            //if (list != null) forecastRepository.RemoveWeatherDataByCity("DenVer");

            //list = forecastRepository.GetWeatherDataByCity("Denver");

            //Assert.AreEqual(0, list.Count);
        }
    }
}
