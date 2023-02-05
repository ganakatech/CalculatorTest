namespace Calculator.Interfaces;

public interface ISimpleCalculator
{
    int Add(int start, int amount);
    int Subtract(int start, int amount);
    int Multiply(int start, int by);
    int Divide(int start, int by);
}