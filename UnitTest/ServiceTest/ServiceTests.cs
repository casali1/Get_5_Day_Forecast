﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Get_5_Day_Forecast.Repository;
using Get_5_Day_Forecast.Model;
using Get_5_Day_Forecast.Service;
using System.Xml.Linq;
using System.IO;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void TestIsValidZip()
        {
            var mock = new Mock<IForecastRepository>();
            mock.Setup(x => x.SaveRequestedData(It.IsAny<AvgDayForecast>()));
            var helper = new Helper(mock.Object);

            var isValid = helper.IsValidZip("80202");
            Assert.IsTrue(isValid);

            isValid = helper.IsValidZip("Denver");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void TestIsValidCity()
        {
            var mock = new Mock<IForecastRepository>();
            mock.Setup(x => x.SaveRequestedData(It.IsAny<AvgDayForecast>()));
            var helper = new Helper(mock.Object);

            var isValid = helper.IsValidCity("Miami-Dade");
            Assert.IsTrue(isValid);

            isValid = helper.IsValidCity("90210");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void TestCalculateAvgTemps()
        {
            var mock = new Mock<IForecastRepository>();
            mock.Setup(x => x.SaveRequestedData(It.IsAny<AvgDayForecast>()));

            var list = new List<DayForecast>();
            list.Add(new DayForecast
            {
                Index = 1,
                Date = Convert.ToDateTime("2019-08-07T03:00:00"),
                MaxTemp = 85M,
                MinTemp = 55M
            });

            list.Add(new DayForecast
            {
                Index = 1,
                Date = Convert.ToDateTime("2019-08-07T06:00:00"),
                MaxTemp = 90M,
                MinTemp = 60M
            });

            list.Add(new DayForecast
            {
                Index = 1,
                Date = Convert.ToDateTime("2019-08-07T09:00:00"),
                MaxTemp = 95M,
                MinTemp = 65M
            });

            var helper = new Helper(mock.Object);
            var calcAvgTemp = helper.CalculateAvgTemps(list, "SomeCity");

            Assert.AreEqual("SomeCity", calcAvgTemp[0].City);
            Assert.AreEqual(7, Convert.ToDateTime(calcAvgTemp[0].Date).Day);
            Assert.AreEqual(90M, calcAvgTemp[0].AvgMaxTemp);
            Assert.AreEqual(60M, calcAvgTemp[0].AvgMinTemp);
        }

        [TestMethod]
        public void TestRetrieveDataFromXML()
        {
            var mock = new Mock<IForecastRepository>();
            mock.Setup(x => x.SaveRequestedData(It.IsAny<AvgDayForecast>()));
            var helper = new Helper(mock.Object);

            var index = Environment.CurrentDirectory.IndexOf("UnitTest");

            var envtDirectory = Environment.CurrentDirectory;
            var currentDirectory = envtDirectory.Substring(0, index);

            var path = Path.Combine(currentDirectory, @"UnitTest\\TestFiles\\WeatherData.xml");

            var weatherData = XElement.Load(path);

            var list = helper.RetrieveDataFromXML("");
        }
    }
}
