using Calculator.Interfaces;

namespace Calculator.Services;

public class NullDiagnostics : IDiagnostics
{
    public void LogResult(string methodName, int result)
    {
        // Do nothing
    }
}