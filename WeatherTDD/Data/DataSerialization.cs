using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace WeatherAPI.Data.DataSerialization
{
    public class WeatherAPIRoot
    {
        public class Forecast
        {
            public double dt { get; set; }
            public Main main { get; set; }
            public IList<Weather> weather { get; set; }
            public Clouds clouds { get; set; }
            public Wind wind { get; set; }
            public Sys sys { get; set; }
            public string dt_txt { get; set; }
        }
        public class Main
        {
            public double temp { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public double pressure { get; set; }
            public double sea_level { get; set; }
            public double grnd_level { get; set; }
            public double humidity { get; set; }
            public double temp_kf { get; set; }
        }
        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        public class Clouds
        {
            public int all { get; set; }
        }
        public class Wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }
        public class Sys
        {
            public string pod { get; set; }
        }
        []
        public class Rain
        {
            [DataMember(nameof = "3h")]
            public double threeh { get; set; } 
        }
        public class City
        {
            public int id { get; set; }
            public string name { get; set; }
            public Coord coord { get; set; }
            public string country { get; set; }
            public int population { get; set; }
            public int timezone { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }
        public class Coord
        {
            public double lat { get; set; }
            public double lon { get; set; }
        }
        public int cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public IList<Forecast> list { get; set; }
        public City city { get; set; }
    }

    class OpenWeatherMapForecastService
    {
        // We need DTO
        public OpenWeatherAPIRoot openWeatherAPIDTO= new OpenWeatherAPIRoot();
        // We need Call Manager
        public WeatherMain WeatherMainCallManager = new WeatherMain();
        public String Parameters { get; set; }
        // We need JObject
        public JObject WeatherMainJson;
        public OpenWeatherMapForecastService()
        {
            openWeatherAPIDTO.DeserializeForecast(WeatherMainCallManager.GetWeatherForcast(Parameters));
            WeatherMainJson = JObject.Parse(WeatherMainCallManager.GetWeatherForcast(Parameters));
        }
    }
    public class OpenWeatherMapForecastDTO
    {
        public OpenWeatherAPIRoot openWeatherMapForecastRoot { get; set; }
        public void DeserializeForecast(String OpenWeatherMapForecastResponse)
        {
            openWeatherMapForecastRoot = JsonConvert.DeserializeObject<OpenWeatherAPIRoot>(OpenWeatherMapForecastResponse);
        }
    }
    public class OpenWeatherAPIRoot
    {
        public WeatherAPIRoot openweatherAPIRoot { get; set; }
        public void DeserializeForecast(String OpenWeatherApiResponse)
        {
            openweatherAPIRoot = JsonConvert.DeserializeObject<WeatherAPIRoot>(OpenWeatherApiResponse);
        }
    }
}