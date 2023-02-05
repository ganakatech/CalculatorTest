using System.Text;
using Calculator;
using Calculator.Data;
using Calculator.Services;
using Newtonsoft.Json;

namespace CalculatorConsole;

public static class Utils
{
    public static async void DemoSimpleCalculatorWithDiagnosticsUsage()
    {
        var databaseDiagnostics = new DatabaseDiagnostics();
        var calculator = new SimpleCalculator(new ConsoleDiagnostics());

        int result = calculator.Add(3, 4);
        Console.WriteLine("Result of Add: " + result);
        databaseDiagnostics.LogResult("Add", result);

        result = calculator.Subtract(7, 3);
        Console.WriteLine("Result of Subtract: " + result);
        databaseDiagnostics.LogResult("Subtract", result);

        result = calculator.Multiply(3, 4);
        Console.WriteLine("Result of Multiply: " + result);
        databaseDiagnostics.LogResult("Multiply", result);

        result = calculator.Divide(10, 2);
        Console.WriteLine("Result of Divide: " + result);
        databaseDiagnostics.LogResult("Divide", result);

        var records = await databaseDiagnostics.GetAllDiagnosticsRecordsAsync();
        foreach (var record in records)
        {
            Console.WriteLine(
                $"Id: {record.Id} Method: {record.MethodName} Result: {record.Result} Timestamp: {record.Timestamp}");
        }

        var recordById = await databaseDiagnostics.GetDiagnosticsRecordByIdAsync(1);
        Console.WriteLine(
            $"Id: {recordById.Id} Method: {recordById.MethodName} Result: {recordById.Result} Timestamp: {recordById.Timestamp}");
    }

    public static void DemoSimpleCalculatorWithStoredProcDiagsUsage()
    {
        var connectionString = "Data Source=(local);Initial Catalog=Diagnostics;Integrated Security=True;";
        var storedProcedureDiagnostics = new StoredProcedureDiagnostics(connectionString);
        var calculator1 = new SimpleCalculator(storedProcedureDiagnostics);
        calculator1.Add(2, 3);
        calculator1.Subtract(5, 3);
        calculator1.Multiply(2, 3);
        calculator1.Divide(6, 3);
        var records = storedProcedureDiagnostics.GetDiagnosticsRecords();
        foreach (var record in records)
        {
            Console.WriteLine(
                $"Id: {record.Id} Method: {record.MethodName} Result: {record.Result} Timestamp: {record.Timestamp}");
        }
    }

    public static async void DemoCalculatorWebApisUsage()
    {
        // Create an instance of HttpClient
        var client = new HttpClient();
        // Set the base address for the web service
        client.BaseAddress = new Uri("https://localhost:7245/api/");
        // Call the subtract operation of the calculator web service
        var response = await client.GetAsync("calculator/subtract?start=20&amount=10");
        // Read the response content
        var resultString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"GET /calculator/api/subtract returned {resultString}");
        Console.WriteLine($"GET /calculator/api/subtract returned status {response.StatusCode}");

        // Call the add operation of the calculator web service
        var addParams = new AddParams()
        {
            Start = 10,
            Amount = 10
        };
        HttpContent requestBody =
            new StringContent(JsonConvert.SerializeObject(addParams), Encoding.UTF8, "application/json");
        response = await client.PostAsync("calculator/add", requestBody);
        // Read the response content
        resultString = await response.Content.ReadAsStringAsync();
        // Output the result
        Console.WriteLine($"POST /calculator/api/add returned {resultString}");
        Console.WriteLine($"POST /calculator/api/add returned status {response.StatusCode}");

        // Call the multiply operation of the calculator web service
        var multiplyParams = new MuliplyParams()
        {
            Start = 10,
            By = 20
        };
        requestBody = new StringContent(JsonConvert.SerializeObject(multiplyParams), Encoding.UTF8, "application/json");
        response = await client.PostAsync("calculator/multiply", requestBody);
        // Read the response content
        resultString = await response.Content.ReadAsStringAsync();
        // Output the result
        Console.WriteLine($"POST /calculator/api/multiply returned {resultString}");
        Console.WriteLine($"POST /calculator/api/multiply returned status {response.StatusCode}");


        // Call the divide operation of the calculator web service
        response = await client.GetAsync("calculator/divide?start=20&by=10");
        // Read the response content
        resultString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"GET /calculator/api/divide returned {resultString}");
        Console.WriteLine($"GET /calculator/api/divide returned status {response.StatusCode}");
    }
}