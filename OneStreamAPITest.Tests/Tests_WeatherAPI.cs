using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using OneStreamAPITest.API.Services;
using OneStreamAPITest.Data;
using OneStreamAPITest.Services;
using System.Net;
using System.Net.Http.Json;

namespace OneStreamAPITest.Tests
{
    public class WeatherApiServiceTests
    {
        [Fact]
        public async Task GetWeatherAsync_ShouldReturnWeatherResponse_WhenCityIsValid()
        {
            // Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            var weatherResponse = new WeatherResponse
            {
                location = new Location { name = "London", country = "UK" },
                current = new Current { temp_c = 15.5f, temp_f = 59.9f }
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
                Content = JsonContent.Create(weatherResponse) // Return the mock response
            });

            var httpClient = new HttpClient(mockMessageHandler.Object);
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(config => config["WeatherApi:ApiKey"]).Returns("test-key");
            mockConfig.Setup(config => config["WeatherApi:BaseUrl"]).Returns("http://api.weatherapi.com/v1/current.json");

            var service = new WeatherAPIService(httpClient, mockConfig.Object);

            // Act
            var result = await service.GetWeatherAsync("London");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("London", result.location.name);
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldReturnNull_WhenApiReturnsError()
        {
            // Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound // Simulate 404 error
            });

            var httpClient = new HttpClient(mockMessageHandler.Object);
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(config => config["WeatherApi:ApiKey"]).Returns("test-key");
            mockConfig.Setup(config => config["WeatherApi:BaseUrl"]).Returns("http://api.weatherapi.com/v1/current.json");

            var service = new WeatherAPIService(httpClient, mockConfig.Object);

            // Act
            var result = await service.GetWeatherAsync("InvalidCity");

            // Assert
            Assert.Null(result);
        }
    }
}