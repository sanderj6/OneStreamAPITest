using Moq;
using Moq.Protected;
using OneStreamAPITest.API.Services;
using OneStreamAPITest.Data;
using System.Net;

namespace OneStreamAPITest.Tests
{
    public class StarWarsApiServiceTests
    {
        [Fact]
        public async Task GetCharacterAsync_ShouldReturnCharacter_WhenIdIsValid()
        {
            // Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            // Example SW Character
            var character = new StarWarsCharacter
            {
                Name = "Luke Skywalker",
                Height = "172",
                Mass = "77",
                HairColor = "blond",
                SkinColor = "fair",
                EyeColor = "blue",
                BirthYear = "19BBY",
                Gender = "male",
                Homeworld = "https://swapi.dev/api/planets/1/"
            };

            mockMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(character)),
                });

            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://swapi.dev/")
            };

            var service = new StarWarsAPIService(httpClient);

            // Act
            var result = await service.GetCharacterAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Luke Skywalker", result.Name);
        }

        [Fact]
        public async Task GetCharacterAsync_ShouldThrowHttpRequestException_WhenApiReturnsError()
        {
            // Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler
             .Protected()
             .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.IsAny<HttpRequestMessage>(),  // Matches any HttpRequestMessage
                 ItExpr.IsAny<CancellationToken>()    // Matches any CancellationToken
             )
             .ReturnsAsync(new HttpResponseMessage
             {
                 StatusCode = HttpStatusCode.NotFound // or other status code
             });

            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://swapi.dev/")
            };

            var service = new StarWarsAPIService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => service.GetCharacterAsync(1));
        }
    }
}