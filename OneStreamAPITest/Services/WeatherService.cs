using OneStreamAPITest.Data;
using System.Net.Http.Json;

namespace OneStreamAPITest.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/weather/{city}");

                response.EnsureSuccessStatusCode();

                var weatherData = await response.Content.ReadFromJsonAsync<WeatherResponse>();

                return weatherData;
            }
            catch (HttpRequestException httpEx)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
