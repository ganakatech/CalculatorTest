using Calculator.Interfaces;

namespace Calculator;

public class SimpleCalculator : ISimpleCalculator
{
    private readonly IDiagnostics? _diagnostics;

    public SimpleCalculator(IDiagnostics? diagnostics = null)
    {
        _diagnostics = diagnostics;
    }
    public int Add(int start, int amount)
    {
        int result = start + amount;
        _diagnostics?.LogResult(nameof(Add), result);
        return result;
    }

    public int Subtract(int start, int amount)
    {
        int result = start - amount;
        _diagnostics?.LogResult(nameof(Subtract), result);
        return result;
    }

    public int Multiply(int start, int by)
    {
        int result = start * by;
        _diagnostics?.LogResult(nameof(Multiply), result);
        return result;
    }

    public int Divide(int start, int by)
    {
        int result = start / by;
        _diagnostics?.LogResult(nameof(Divide), result);
        return result;
    }
}
