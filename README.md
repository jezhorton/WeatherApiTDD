# OpenWeatherMap API Test
## Table of Contents
* [Introduction](#introduction)
* [Implementation](#implementation)
* [Testing](#testing)
* [NuGet Packages](#nuget-packages)
* [Conclusion](#conclusion)
## Introduction
API testing for the [OpenWeatherMap.org](https://openweathermap.org/api) 5 day / 3 hour weather forecast. 

The aim of this project was to test the GET response output (in JSON) from the OpenWeatherMap API to check the recieved responses' validity. 

All tests were based on the weather forecast for London.

## Instructions

1. Clone this git repository ```https://github.com/jezhorton/WeatherApiTDD```
2. Open OpenWeatherMapTest.sln 
3. Right click and build 'OpenWeatherMapTest' Solution in the Solution Explorer
4. Right click on the 'OpenWeatherMapTest' Project file in the Solution Explorer and click 'Manage NuGet Packages..." 
5. Update NuGet packages/Check that all the packages are installed and updated

## NuGet Packages

 - [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) - Json .Net is a popular high-performance JSON framework for .NET
 - [NUnit](https://www.nuget.org/packages/NUnit/) - NUnit is a unit-testing framework for all .Net languages with a strong TDD focus.
 - [NUnit3TestAdapter](https://www.nuget.org/packages/NUnit3TestAdapter/) - NUnit 3 adapter for running tests in Visual Studio.
 - [RestSharp](https://www.nuget.org/packages/RestSharp/) - Simple REST and HTTP API Client

## Implementation
For the implementation of the testing I used a DTO serialization model this was used in order to take the raw JSON format and parse it into the code aspects of the code.
### DataSerialization
Within this file are a series of classes that do a variety of different things. 
#### WeatherAPIROOT
This includes the `get; set;`for each of the different attributes and keys within the JSON file to be accessed later on. I also included the serialization used from Newsoft.JSON.
##### OpenWeatherMapForcast
This was the calling of the DTO, call manager and JObjects. This is where the serialization of the objects take place using `JObject.Parse`
##### OpenWeatherMapForcastDTO
This is was the main reference to deserialize the forcast using `JsonConvert.DeserializeObject
`
##### OpenWeatherAPIRoot 
This was the class that was used during most of the testing in order to access all of the attributes, this class was the gateway into the deserialized JSON
## Testing
 [Postman](https://www.getpostman.com/) was used to view the response received from the API.
### JSON Example of API Response(From PostMan):
```JSON
    {
        "cod":  "200",        
        "message":  0.0121,        
        "cnt":  40,        
        "list":  [        
	        {        
		        "dt":  1570676400,       
		        "main":  {        
			        "temp":  281.27,        
			        "temp_min":  281.27,        
			        "temp_max":  282.63,        
			        "pressure":  1008.14,        
			        "sea_level":  1008.14,        
			        "grnd_level":  1003.37,        
			        "humidity":  80,        
			        "temp_kf":  -1.36        
			    },        
		        "weather":  [       
			        {        
				        "id":  800,        
				        "main":  "Clear",        
				        "description":  "clear sky",        
				        "icon":  "01n"        
			        }        
		        ],        
		        "clouds":  {        
			        "all":  0        
		        },        
		        "wind":  {        
			        "speed":  5.2,        
			        "deg":  260.476        
		        },        
		        "sys":  {        
			        "pod":  "n"        
		        },        
    			"dt_txt":  "2019-10-10 03:00:00"
	    	}
    	],
    	"city":  {    
	    "id":  2643743,    
	    "name":  "London",    
	    "coord":  {    
		    "lat":  51.5085,    
		    "lon":  -0.1258    
	    },    
	    "country":  "GB",    
	    "population":  1000000,    
	    "timezone":  3600,    
	    "sunrise":  1570688109,    
	    "sunset":  1570727985    
	    }    
	}
```
All data points would be tested as testing the content is the most important thing to test, the headers would also be tested to check the content type, connection and server.
### TestMain
Creates an instance of the service in order to run tests against it
### Tests run on data points
#### root
 - "cod" - Check for a successful web call 200
 - "message" - Check message greater than 0
 - "cnt" - Check number of lines returned is greater than 0

#### list
 Note: List is an array, only the first object in the array was tested
 - "dt" - Check date is correct length
 
**main**

*Check the temperature is within +/- 100 	degrees Kelvin
 - "temp" - Check within valid Kelvin range between the minimum and the maximum
 - "temp_min" - Check within valid Kelvin range below the maximum range
 - "temp_max" - Check within valid Kelvin range above the minimum range
 
*Atmospheric pressure in hPa (hectopascal), check within +/- 150 from 1000 hPa*
 - "pressure" - Check within valid pressure range at sea level by default
 - "sea_level" - Check within valid pressure range at sea level
 - "grnd_level" - Check within valid pressure range at ground level
 - "humidity" - Measured in %. Check that it is between 0 and 100
 - "temp_kf" - Check not null
 
**weather**

Note: Weather is an array, only the first object in the array was tested
 - "id" - Check that id is a valid weather code ([Weather Codes](https://openweathermap.org/weather-conditions))
 - "main" - Check not null
 - "description" - Check not null
 - "icon" - Check not null
 
**clouds**

 - "all" - Measured in %. Check that it is between 0 and 100
 
**wind**

- "speed" - Check wind speed is greater than or equal to 0
- "deg" - Check that wind direction is between 0 and 360 degrees

**rain**

- "3h" - Serialized at runtime as variables cannot begin with a number. Since it may not have rained a null reference is checked first before then checking if rain is greater than or equal to 0

**sys** 

- "pod" - Check not null
 - "dt_text" - Check date is correct length
#### city
- "id" - Check not null
- "name" - Check not null

## Conclusion
Ideally testing the different aspects of headers as well as the different aspects of all of the code. I would of liked to add in some additional functionality into dynamically updating the position of the object in test.
