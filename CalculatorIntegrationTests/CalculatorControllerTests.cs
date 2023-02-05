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
    public async Task Get_Addition_ShouldReturnCorrectResult()
    {
        // Arrange
        var request = new { FirstNumber = 2, SecondNumber = 3 };
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.GetAsync("api/calculator/add?FirstNumber=2&SecondNumber=3");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = double.Parse(responseString);

        // Assert
        Assert.Equal(5, result);
    }

}