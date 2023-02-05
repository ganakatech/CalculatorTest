// See https://aka.ms/new-console-template for more information

using CalculatorConsole;

try
{
    Console.WriteLine("========= Demo SimpleCalculator with diagnostics Usage =========");
    Utils.DemoSimpleCalculatorWithDiagnosticsUsage();
    Console.WriteLine();

    
    Console.WriteLine("========= Demo SimpleCalculator with stored procedure diagnostics Usage =========");
    //Utils.DemoSimpleCalculatorWithStoredProcDiagsUsage();
    Console.WriteLine();

    Console.WriteLine("========= Demo Calculator Web API Usage =========");
    Utils.DemoCalculatorWebApisUsage();
    Console.WriteLine();
}
catch (Exception e)
{
    Console.WriteLine($"Exception with message : {e.Message}");
}

Console.ReadLine();