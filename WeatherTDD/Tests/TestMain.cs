using System;
using NUnit.Framework;
using WeatherAPI.Data.DataSerialization;

namespace WeatherAPI.Test
{
    [TestFixture]
    public class MainTest
    {
        OpenWeatherMapForecastService openWeatherMapForecastService = new OpenWeatherMapForecastService();
        public MainTest()
        {
            openWeatherMapForecastService.Parameters = "q=London,gb";
        }
        // Check for successful web call 200
        [Test]
        public void WebCallSuccessCheck()
        {
            Assert.AreEqual(200, Data.DataSerialization.WeatherAPIRoot.cod);
        }
        [Test]
        public void TestWeatherTemp()
        {
            Assert.AreEqual(200, WeatherAPIRoot.cod);
        }
        [Test]
        public void TestWeatherTemp_Min()
        {
            Assert.Pass();
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
