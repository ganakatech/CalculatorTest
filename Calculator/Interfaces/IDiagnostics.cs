namespace Calculator.Interfaces;

public interface IDiagnostics
{
    void LogResult(string methodName, int result);
}