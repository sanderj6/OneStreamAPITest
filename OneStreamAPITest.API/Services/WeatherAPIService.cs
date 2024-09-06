using OneStreamAPITest.Data;

namespace OneStreamAPITest.API.Services
{
    public class WeatherAPIService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public WeatherAPIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            var apiKey = _configuration["WeatherApi:ApiKey"];
            var baseUrl = _configuration["WeatherApi:BaseUrl"];

            try
            {
                var response = await _httpClient.GetFromJsonAsync<WeatherResponse>($"{baseUrl}?key={apiKey}&q={city}&aqi=no");
                return response;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
