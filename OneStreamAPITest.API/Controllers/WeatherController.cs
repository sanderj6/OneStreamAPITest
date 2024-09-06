using Microsoft.AspNetCore.Mvc;
using OneStreamAPITest.API.Services;
using System.Net.Http;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly WeatherAPIService _weatherApiService;

    public WeatherController(WeatherAPIService weatherApiService)
    {
        _weatherApiService = weatherApiService;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        try
        {
            var response = await _weatherApiService.GetWeatherAsync(city);
            return Ok(response);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error fetching character data: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }
}
