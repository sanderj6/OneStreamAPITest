using System.Net.Http.Json;
using OneStreamAPITest.Data;

namespace OneStreamAPITest.Services
{
    public class StarWarsService
    {
        private readonly HttpClient _httpClient;

        public StarWarsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StarWarsCharacter> GetCharacterAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/starwars/character/{id}");

                response.EnsureSuccessStatusCode();

                var characterData = await response.Content.ReadFromJsonAsync<StarWarsCharacter>();

                return characterData;
            }
            catch (HttpRequestException httpEx)
            {
                throw new ApplicationException($"Error fetching character data: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
