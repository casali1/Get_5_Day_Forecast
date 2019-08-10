using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Get_5_Day_Forecast.Repository;
using Get_5_Day_Forecast.Model;
using Get_5_Day_Forecast.Service;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class ServiceTests
    {
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
        public void TestIsCity()
        {
            var mock = new Mock<IForecastRepository>();
            mock.Setup(x => x.SaveRequestedData(It.IsAny<AvgDayForecast>()));
            var helper = new Helper(mock.Object);

            var isValid = helper.IsCity("Miami-Dade");
            Assert.IsTrue(isValid);

            isValid = helper.IsCity("90210");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void TestIsZip()
        {
            var mock = new Mock<IForecastRepository>();
            mock.Setup(x => x.SaveRequestedData(It.IsAny<AvgDayForecast>()));
            var helper = new Helper(mock.Object);

            var isValid = helper.IsCity("80202");
            Assert.IsTrue(isValid);

            isValid = helper.IsZipCode("Denver");
            Assert.IsFalse(isValid);           
        }

    }
}
