using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public WeatherController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        var response = await _httpClient.GetStringAsync($"http://api.weatherapi.com/v1/current.json?key=7f8a7cca121043718a0144317240609&q={city}&aqi=no");
        return Ok(response);
    }
}
