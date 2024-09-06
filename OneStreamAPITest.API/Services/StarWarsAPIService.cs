using OneStreamAPITest.Data;

namespace OneStreamAPITest.API.Services
{
    public class StarWarsAPIService
    {
        private readonly HttpClient _httpClient;

        public StarWarsAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StarWarsCharacter> GetCharacterAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://swapi.dev/api/people/{id}/");

            response.EnsureSuccessStatusCode();

            var characterData = await response.Content.ReadFromJsonAsync<StarWarsCharacter>();

            return characterData;
        }

    }
}
