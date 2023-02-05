using Calculator.Interfaces;

namespace Calculator.Services;

public class ConsoleDiagnostics : IDiagnostics
{
    public void LogResult(string methodName, int result)
    {
        Console.WriteLine($"{methodName} returned {result}");
    }
}