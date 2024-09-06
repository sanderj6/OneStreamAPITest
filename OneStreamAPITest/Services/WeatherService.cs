namespace OneStreamAPITest.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            return await _httpClient.GetStringAsync($"api/weather/{city}");
        }
    }
}
