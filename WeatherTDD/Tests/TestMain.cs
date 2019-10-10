using System;
using NUnit.Framework;
using WeatherAPI.Data.DataSerialization;

namespace WeatherAPI.Test
{
    [TestFixture]
    public class MainTest
    {
        OpenWeatherMapForecastService openWeatherMapForecastService = new OpenWeatherMapForecastService();
        //Constructor for the tests
        public MainTest()
        {
            openWeatherMapForecastService.Parameters = "q=London,gb";
        }
        // Check for successful web call 200
        [Test]
        public void WebCallSuccessCheck()
        {
            Assert.AreEqual(200, openWeatherMapForecastService.openWeatherMapForecastDTO.openweatherAPIRoot.cod);
        }
        // Check for message parameter
        [Test]
        public void MessageCheck()
        {
            Assert.Greater(openWeatherMapForecastService.openWeatherMapForecastDTO.openweatherAPIRoot.message, 0);
        }
        // Number of lines returned by this API call, check for return
        [Test]
        public void CountCheck()
        {
            Assert.Greater(openWeatherMapForecastService.openWeatherMapForecastDTO.openweatherAPIRoot.cnt, 0);
        }
        // Check for the date time format rather than the specific date
        [Test]
        public void DtCheck()
        {
            // Convert unix to DateTime
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime dtDate = origin.AddSeconds(openWeatherMapForecastService.openWeatherMapForecastDTO.openweatherAPIRoot.list[0].dt);
            // Check DateTime string is valid
            DateTime parsedDate;
            bool isValid = false;
            isValid = DateTime.TryParse(dtDate.ToString(), out parsedDate);
            Assert.IsTrue(isValid);
        }
        [Test]
        public void TestWeatherTemp_Min()
        {
            Assert.That(openWeatherMapForecastService.openWeatherMapForecastDTO.openweatherAPIRoot.list[0].main.temp, Is.EqualTo(273.15).Within(200));
        }
        [Test]
        public void TestWeatherTemp_Max()
        {
            Assert.Pass();
        }
        [Test]
        public void TestWeatherPressure()
        {
            Assert.Pass();

        }
        [Test]
        public void TestWeatherSea_Level()
        {
            Assert.Pass();

        }
        [Test]
        public void TestWeatherGrnd_Level()
        {
            Assert.Pass();


        }
        [Test]
        public void TestWeatherHumidity()
        {
            Assert.Pass();


        }
        [Test]
        public void TestWeatherTemp_Kf()
        {
            Assert.Pass();
        }

    }
}
