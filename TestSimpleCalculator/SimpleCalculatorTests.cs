using Calculator;
using Calculator.Interfaces;
using Moq;

namespace TestCalculator;

[TestFixture]
public class SimpleCalculatorTests
{
    private SimpleCalculator? _simpleCalculator;
    private Mock<IDiagnostics>? _diagnosticsMock;

    [SetUp]
    public void Setup()
    {
        _diagnosticsMock = new Mock<IDiagnostics>();
        _simpleCalculator = new SimpleCalculator(_diagnosticsMock!.Object);
    }

    [Test]
    public void Add_TwoPositiveNumbers_ReturnsCorrectResult()
    {
        // Arrange
        int start = 50;
        int amount = 30;
        int expectedResult = 80;

        // Act
        int result = _simpleCalculator!.Add(start, amount);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Subtract_TwoPositiveNumbers_ReturnsCorrectResult()
    {
        // Arrange
        int start = 50;
        int amount = 30;
        int expectedResult = 20;

        // Act
        int result = _simpleCalculator!.Subtract(start, amount);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Multiply_TwoPositiveNumbers_ReturnsCorrectResult()
    {
        // Arrange
        int start = 50;
        int by = 30;
        int expectedResult = 1500;

        // Act
        int result = _simpleCalculator!.Multiply(start, by);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Divide_TwoPositiveNumbers_ReturnsCorrectResult()
    {
        // Arrange
        int start = 60;
        int by = 20;
        int expectedResult = 3;

        // Act
        int result = _simpleCalculator!.Divide(start, by);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Add_TwoNegativeNumbers_ReturnsCorrectResult()
    {
        // Arrange
        int start = -5;
        int amount = -3;
        int expectedResult = -8;

        // Act
        int result = _simpleCalculator!.Add(start, amount);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
    
    [Test]
    public void Add_AmountZero_ReturnsResultAsStart()
    {
        // Arrange
        int start = 5;
        int amount = 0;
        
        // Act
        int result = _simpleCalculator!.Add(start, amount);

        // Assert
        Assert.That(result, Is.EqualTo(start));
    }

    [Test]
    public void Divide_ByZero_ThrowsException()
    {
        // Arrange
        int start = 6;
        int by = 0;

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() =>
        {
            _simpleCalculator!.Divide(start, by);
        });
    }
    
    [Test]
    public void Divide_WithStartZero_ResultsAsZero()
    {
        // Arrange
        int start = 0;
        int by = 1;

        // Act & Assert
        Assert.That(_simpleCalculator!.Divide(start, by), Is.EqualTo(0));
    }
    
    [Test]
    public void Multiply_WithStartZero_ResultsAsZero()
    {
        // Arrange
        int start = 0;
        int by = 1;

        // Act & Assert
        Assert.That(_simpleCalculator!.Multiply(start, by), Is.EqualTo(0));
    }
    
    [Test]
    public void Multiply_WithByZero_ResultsAsZero()
    {
        // Arrange
        int start = 1;
        int by = 0;

        // Act & Assert
        Assert.That(_simpleCalculator!.Multiply(start, by), Is.EqualTo(0));
    }

    [Test]
    public void Divide_TwoLargeNumbers_DoesNotOverflow()
    {
        // Arrange
        int start = int.MaxValue;
        int by = 2;
        int expectedResult = int.MaxValue / 2;

        // Act
        int result = _simpleCalculator!.Divide(start, by);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Add_TwoPositiveNumbersWithLargeSum_DoesNotOverflow()
    {
        // Arrange
        int start = int.MaxValue;
        int amount = 1;

        // Act & Assert
        Assert.DoesNotThrow(() => { _simpleCalculator!.Add(start, amount); });
    }

    [Test]
    public void Subtract_TwoLargeNumbers_DoesNotUnderflow()
    {
        // Arrange
        int start = int.MinValue;
        int amount = -1;

        // Act & Assert
        Assert.DoesNotThrow(() => { _simpleCalculator!.Subtract(start, amount); });
    }

    [Test]
    public void Multiply_TwoLargeNumbers_DoesNotOverflow()
    {
        // Arrange
        int start = int.MaxValue;
        int by = 2;

        // Act & Assert
        Assert.DoesNotThrow(() => { _simpleCalculator!.Multiply(start, by); });
    }
    
    [Test]
    public void Add_ValidInput_DiagnosticsCalledWithExpectedValues()
    {
        // Arrange
        int start = 2, amount = 3;
        int expectedResult = start + amount;
        string expectedMethodName = nameof(_simpleCalculator.Add);

        // Act
        int result = _simpleCalculator!.Add(start, amount);

        // Assert
        _diagnosticsMock!.Verify(d => d.LogResult(expectedMethodName, expectedResult), Times.Once());
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Subtract_ValidInput_DiagnosticsCalledWithExpectedValues()
    {
        // Arrange
        int start = 7, amount = 3;
        int expectedResult = start - amount;
        string expectedMethodName = nameof(_simpleCalculator.Subtract);

        // Act
        int result = _simpleCalculator!.Subtract(start, amount);

        // Assert
        _diagnosticsMock!.Verify(d => d.LogResult(expectedMethodName, expectedResult), Times.Once());
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Multiply_ValidInput_DiagnosticsCalledWithExpectedValues()
    {
        // Arrange
        int start = 2, by = 3;
        int expectedResult = start * by;
        string expectedMethodName = nameof(_simpleCalculator.Multiply);

        // Act
        int result = _simpleCalculator!.Multiply(start, by);

        // Assert
        _diagnosticsMock!.Verify(d => d.LogResult(expectedMethodName, expectedResult), Times.Once());
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Divide_ValidInput_DiagnosticsCalledWithExpectedValues()
    {
        // Arrange
        int start = 10, by = 2;
        int expectedResult = start / by;
        string expectedMethodName = nameof(_simpleCalculator.Divide);

        // Act
        int result = _simpleCalculator!.Divide(start, by);

        // Assert
        _diagnosticsMock!.Verify(d => d.LogResult(expectedMethodName, expectedResult), Times.Once());
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}