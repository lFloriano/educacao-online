using EducacaoOnline.IntegrationTests.Factories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace EducacaoOnline.IntegrationTests
{
    public class HealthControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _httpClient;
        public HealthControllerTests()
        {
            var factory = new CustomWebApplicationFactory();
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Health_ReturnsOk()
        {
            //arrange

            //act
            var response = await _httpClient.GetAsync("/api/health");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be("API OK");
        }

    }
}
