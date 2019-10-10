using System;
using NUnit.Framework;
using WeatherAPI.Data.DataSerialization;

namespace WeatherAPI.Test
{
    [TestFixture]
    public class MainTest
    {
        OpenWeatherMapForecastService openWeatherAPI = new OpenWeatherMapForecastService();
        //Constructor for the tests
        public MainTest()
        {
            openWeatherAPI.Parameters = "q=London,gb";
        }
        // Check for successful web call 200
        [Test]
        public void WebCallSuccessCheck()
        {
            Assert.AreEqual(200, openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.cod);
        }
        // Check for message parameter
        [Test]
        public void MessageCheck()
        {
            Assert.Greater(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.message, 0);
        }
        // Number of lines returned by this API call, check for return
        [Test]
        public void CountCheck()
        {
            Assert.Greater(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.cnt, 0);
        }
        // Check for the date time format rather than the specific date
        [Test]
        public void DtCheck()
        {
            // Convert unix to DateTime
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime dtDate = origin.AddSeconds(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].dt);
            // Check DateTime string is valid
            DateTime parsedDate;
            bool isValid = false;
            isValid = DateTime.TryParse(dtDate.ToString(), out parsedDate);
            Assert.IsTrue(isValid);
        }
        //Testing exact weather temp (within 100)
        [Test]
        public void TestWeatherTemp()
        {
            Assert.That(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.temp, Is.EqualTo(273.15).Within(100));
        }
        //Testing the minimum is less than the maximum
        [Test]
        public void TestWeatherMinByMax()
        {
            Assert.That(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.temp_min, Is.LessThan(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.temp_max));
        }
        // Checking the maximum is above the minimum
        [Test]
        public void TestWeatherTemp_Max()
        {
            Assert.That(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.temp_max, Is.AtLeast(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.temp_min));
        }
        // Checking the pressure within 150 points of 1000 (the current forcasted pressure)
        [Test]
        public void TestWeatherPressure()
        {
            Assert.That(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.pressure, Is.EqualTo(1000).Within(150));

        }
        [Test]
        public void TestWeatherSea_Level()
        {
            Assert.That(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.sea_level, Is.EqualTo(1000).Within(150));
        }
        [Test]
        public void TestWeatherGrnd_Level()
        {
            Assert.That(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.grnd_level, Is.EqualTo(1000).Within(150));
        }
        //Checking the percentage of humidity is between 0 and 100
        [Test]
        public void TestWeatherHumidity()
        {
            double humidity = openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.humidity;
            Assert.IsTrue(humidity >= 0 && humidity <= 100);
        }
        [Test]
        public void TestWeatherTemp_Kf()
        {
            Assert.NotNull(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].main.temp_kf);
        }
    }
    [TestFixture]
    public class WeatherArrayTest
    {
        OpenWeatherMapForecastService openWeatherAPI = new OpenWeatherMapForecastService();
        //Constructor for the tests
        public WeatherArrayTest()
        {
            openWeatherAPI.Parameters = "q=London,gb";
        }
        // A test to check the weather code is one of the pre defined ones in the api
        [Test]
        public void TestWeatherCode()
        {
            int[] test = {                 // The weather codes gathered from the api documentation
                200, 201, 202, 210, 211, 212, 221, 230, 231, 232,
                300, 301, 302, 310, 311, 312, 313, 314, 321,
                500, 501, 502, 503, 504, 511, 520, 521, 521, 522, 531,
                600, 601, 602, 611, 612, 613, 615, 616, 620, 621, 622,
                701, 711, 721, 731, 741, 751, 761, 762, 771, 781,
                800, 801, 802, 803, 804 };
            Assert.Contains(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].weather[0], (test));
        }
        // A test to check there is a weather code
        [Test]
        public void TestWeatherCodeNotNull()
        {
            Assert.NotNull(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].weather[0]);
        }
        //Checking all of the different null values in all of the string values
        [Test]
        public void TestWeatherMainNotNull()
        {
            Assert.NotNull(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].weather[1]);
        }
        [Test]
        public void TestWeatherDescNotNull()
        {
            Assert.NotNull(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].weather[2]);
        }
        [Test]
        public void TestWeatherIconNotNull()
        {
            Assert.NotNull(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].weather[3]);
        }
    }
    [TestFixture]
    public class TestSmallMethods
    {
        OpenWeatherMapForecastService openWeatherAPI = new OpenWeatherMapForecastService();
        //Constructor for the tests
        public TestSmallMethods()
        {
            openWeatherAPI.Parameters = "q=London,gb";
        }
        [Test]
        public void TestClouds()
        {
            double clouds = openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].clouds.all;
            Assert.IsTrue(clouds >= 0 && clouds <= 100);
        }
        [Test]
        public void TestWindSpeed()
        {
            Assert.That(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].wind.speed, Is.EqualTo(5).Within(5));
        }
        [Test]
        public void TestWindDegree()
        {
            double windSpeedValue = openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].wind.deg;
            Assert.IsTrue(windSpeedValue >= 0 && windSpeedValue <= 360);
        }
        [Test]
        public void TestRain()
        {
            bool raining = false;
            if (openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].rain.threeh >= 0)
            {
                raining = true;
                Assert.IsTrue(raining);
            }
            else
            {
                Assert.Pass();
            }
        }
        [Test]
        public void TestPod()
        {
            Assert.NotNull(openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].sys.pod);
        }
        [Test]
        public void TestDateTimeSerial()
        {
            Assert.AreEqual(19, openWeatherAPI.openWeatherAPIDTO.openweatherAPIRoot.list[0].dt_txt);
        }
    }
}
