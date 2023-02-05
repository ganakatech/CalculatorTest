using CalculatorWebAPI.Controllers;
using CalculatorWebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestCalculator.Controllers;

public class CalculatorControllerTests
{
    private Mock<ILogger<CalculatorController>>? _logger;
    private CalculatorController? _calculatorController;
    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CalculatorController>>();
        _calculatorController = new CalculatorController(_logger.Object);
    }

    [Test]
    public void Add_ShouldReturnCorrectResult()
    {
        // Arrange
        int expectedResult = 50;
        var addParams = new AddParams()
        {
            Start = 20,
            Amount = 30
        };

        // Act
        var result = _calculatorController!.Add(addParams) as ObjectResult;

        // Assert
        Assert.That(result!.Value, Is.EqualTo(expectedResult));
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    
    [Test]
    public void Subtract_ShouldReturnCorrectResult()
    {
        // Arrange
        int start = 50;
        int amount = 30;
        int expectedResult = 20;

        // Act
        var result = _calculatorController!.Subtract(start,amount) as ObjectResult;

        // Assert
        Assert.That(result!.Value, Is.EqualTo(expectedResult));
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }

    [Test] public void Multiply_ShouldReturnCorrectResult()
    {
        // Arrange
        int expectedResult = 50;
        var multiplyParams = new MuliplyParams()
        {
            Start = 2,
            By = 25
        };

        // Act
        var result = _calculatorController!.Multiply(multiplyParams) as ObjectResult;

        // Assert
        Assert.That(result!.Value, Is.EqualTo(expectedResult));
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    
    [Test]
    public void Divide_ShouldReturnCorrectResult()
    {
        // Arrange
        int start = 50;
        int by = 25;
        int expectedResult = 2;

        // Act
        var result = _calculatorController!.Divide(start,by) as ObjectResult;

        // Assert
        Assert.That(result!.Value, Is.EqualTo(expectedResult));
        Assert.That(result.StatusCode, Is.EqualTo(200));
    }
    
    [Test]
    public void Divide_ByZero_ShouldReturnCorrectResult()
    {
        // Arrange
        int start = 50;
        int by = 0;

        // Act
        var result = _calculatorController!.Divide(start,by) as ObjectResult;

        // Assert
        Assert.That(result!.StatusCode, Is.EqualTo(400));
    }
}