using Microsoft.AspNetCore.Mvc;
using OneStreamAPITest.API.Services;
using OneStreamAPITest.Data;

[ApiController]
[Route("api/starwars")]
public class StarWarsController : ControllerBase
{
    private readonly StarWarsAPIService _starWarsApiService;

    public StarWarsController(StarWarsAPIService starWarsApiService)
    {
        _starWarsApiService = starWarsApiService;
    }

    [HttpGet("character/{id}")]
    public async Task<ActionResult<StarWarsCharacter>> GetCharacter(int id)
    {
        try
        {
            var character = await _starWarsApiService.GetCharacterAsync(id);
            return Ok(character);
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