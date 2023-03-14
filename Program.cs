using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherInfoTool
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a city as an argument.");
                return;
            }

            string city = args[0];

            // Read city data from file
            string jsonFilePath = "city_data.json";
            string cityData = await File.ReadAllTextAsync(jsonFilePath);
            CityInfo[] cities = JsonSerializer.Deserialize<CityInfo[]>(cityData);

            // Find city in data array (case-insensitive search)
            CityInfo? cityObj = cities.FirstOrDefault(c => string.Equals(c.city, city, StringComparison.OrdinalIgnoreCase));

            if (cityObj == null)
            {
                Console.WriteLine($"City '{city}' not found in data file.");
                return;
            }

            string latitude = cityObj.lat;
            string longitude = cityObj.lng;

            // Make API call to get weather information
            string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response and extract the weather information
                    // Example code for parsing JSON: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-core-3-1#deserialize-json-into-a-net-object
                    JsonDocument jsonDoc2 = JsonDocument.Parse(responseBody);

                    JsonElement root2 = jsonDoc2.RootElement;
                    double temperature = root2.GetProperty("current_weather").GetProperty("temperature").GetDouble();
                    double windSpeed = root2.GetProperty("current_weather").GetProperty("windspeed").GetDouble();
                    double windDirection = root2.GetProperty("current_weather").GetProperty("winddirection").GetDouble();
                    double weatherCode = root2.GetProperty("current_weather").GetProperty("weathercode").GetDouble();
                    string Time = root2.GetProperty("current_weather").GetProperty("time").GetString();

                    Console.WriteLine($"Temperature: {temperature}°C");
                    Console.WriteLine($"Wind Speed: {windSpeed} km/h");
                    Console.WriteLine($"Wind Direction: {windDirection}");
                    Console.WriteLine($"Weather Code: {weatherCode}");
                    Console.WriteLine($"Time: {Time}");

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error fetching weather information: {e.Message}");
                }
            }
        }

        private class CityInfo
        {
            public string? city { get; set; } = "";
            public string? lat { get; set; } = "";
            public string? lng { get; set; } = "";
        }
    }
}