using System;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace WeatherAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
            WeatherMain main = new WeatherMain();
        }
    }
    class Menu
    {

    }
    public static class WeatherRequest
    {
        public static string BaseUri() => ConfigurationManager.AppSettings["base_uri"];
        public static string AppKey() => ConfigurationManager.AppSettings["App_Key"];
        public static string DefaultUri = ConfigurationManager.AppSettings["base_uri"];
        public static string ApiKey = ConfigurationManager.AppSettings["App_Key"];
        public static string keyMod = ConfigurationManager.AppSettings["keyMod"];
    }
    public class WeatherMain
    {
        public RestClient Client { get; set; }
        public static JObject WeatherMainResponseContent { get; set; }
        public static string WeatherMainSelected { get; set; }
        public WeatherMain() => Client = new RestClient
        {
            BaseUrl = new Uri(WeatherRequest.BaseUri())
        };
        public void GetMainTemp(string temperature)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            WeatherMainSelected = temperature;
            request.Resource = $"postcodes/{temperature.ToLower().Replace(" ", "")}";
            IRestResponse response = Client.Execute(request);
            WeatherMainResponseContent = JObject.Parse(response.Content);
        }

        public string GetWeatherForcast(string parameters)
        {
            var request = new RestRequest(WeatherRequest.DefaultUri + "data/2.5/forecast?" + "q=London,gb" + "&" + WeatherRequest.keyMod + WeatherRequest.ApiKey);
            var response = Client.Execute(request, Method.GET);
            return response.Content;
        }
    }
}
