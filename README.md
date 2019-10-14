Needs update
# WeatherApiTDD
Testing the open weather API

# Nuget Packages
Newsoft.JSon - This is to convert the json recieved from the api and readable by the code
Nunit - This is the unit testing I used
RestSharp - This is the ability to call the api using the restful principles
# Main Tests
Web call success check - This was just checking the message sent and recieved
Message Check check the message parameter
Count Check - Checking the number of lines returned by the api
Dtcheck - Date time check - this is checking the format of the date time so any valid time is acceptable to be able to test at any time
Test Weather Temp - Checking the exact weather temp in kelvin in a segree of closeness (feasible outliars)
Test Maximum Temp - Checking the max temp is above the minimum temp
Test Minimum Temp - Checkign the min temp is above the minimum temp
Test Pressure, sea level and ground level - Testing the correspending values to be within a degree of the basic value
Test Humidity - Checking the humidity to see if it is within 100% and 0%
# Weather Array Tests
Test the code of the current weather - Using an array to list all the possible values as from the website to check the current code against
Testing the code is not null
Basic tests for the string segments to check they are not returned null, this means they can contain any string
