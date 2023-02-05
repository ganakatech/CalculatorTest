using System.Text;
using Newtonsoft.Json;

namespace CalculatorIntegrationTests;

public class CalculatorControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CalculatorControllerTests(CustomWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Subtract_ShouldReturnCorrectResult()
    {
        // Arrange
        // Act
        var response = await _client.GetAsync("api/calculator/subtract?start=3&amount=3");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = double.Parse(responseString);

        // Assert
        Assert.Equal(0, result);
    }

}