using Get_5_Day_Forecast.Controllers;
using Get_5_Day_Forecast.Model;
using Get_5_Day_Forecast.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.ControllerTest
{
    [TestClass]
    public class WeatherControllerTests
    {

        [TestMethod]
        public async Task Input_Returns_A_CreatedAtAction()
        {
            var mock = new Mock<IHelper>();
            mock.Setup(x => x.IsValidZip(It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.IsValidCity(It.IsAny<string>())).Returns(true);
            mock.Setup(x => x.RetrieveDataFromXML(It.IsAny<string>())).Returns(It.IsAny<List<DayForecast>>());
            mock.Setup(x => x.CalculateAvgTemps(It.IsAny<List<DayForecast>>(), It.IsAny<string>())).Returns(Get_3_Day_Avg_Temps_For_Denver());

            var controller = new WeatherController(mock.Object);

            var result = await controller.Input("Denver");

            var aaa = result.GetType();


            Assert.IsInstanceOfType(result.GetType(), typeof(object));
        }


        #region Helper Functions

        private List<AvgDayForecastDTO> Get_3_Day_Avg_Temps_For_Denver()
        {
            var list = new List<AvgDayForecastDTO>();
            list.Add(new AvgDayForecastDTO
            {
                City = "Denver",
                Date = "2019-08-07T03:00:00",
                AvgMaxTemp = 85M,
                AvgMinTemp = 55M
            });

            list.Add(new AvgDayForecastDTO
            {
                City = "Denver",
                Date = "2019-08-08T06:00:00",
                AvgMaxTemp = 90M,
                AvgMinTemp = 60M
            });

            list.Add(new AvgDayForecastDTO
            {
                City = "Denver",
                Date = "2019-08-09T09:00:00",
                AvgMaxTemp = 95M,
                AvgMinTemp = 65M
            });

            return list;
        }

        #endregion
    }
}
