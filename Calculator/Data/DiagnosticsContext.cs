using Calculator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Data;

public class DiagnosticsContext : DbContext
{
    public DbSet<DiagnosticsRecord>? DiagnosticsRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=DiagnosticsDB;Integrated Security=True;");
        optionsBuilder.UseInMemoryDatabase(databaseName: "DiagnosticsDB");
    }
}

public class DiagnosticsRecord
{
    public int Id { get; set; }
    public string? MethodName { get; set; }
    public string? Input { get; set; }
    public int Result { get; set; }
    public DateTime Timestamp { get; set; }
}

public class DatabaseDiagnostics : IDiagnostics
{
    public async void LogResult(string methodName, int result)
    {
        try
        {
            var context = new DiagnosticsContext();
            var record = new DiagnosticsRecord
            {
                MethodName = methodName,
                Result = result,
                Timestamp = DateTime.Now
            };
            await context.DiagnosticsRecords!.AddAsync(record);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception in LogResult with message {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<DiagnosticsRecord>> GetAllDiagnosticsRecordsAsync()
    {
        try
        {
            var context = new DiagnosticsContext();
            return await context.DiagnosticsRecords!.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception in GetAllDiagnosticsRecordsAsync with message {e.Message}");
            throw;
        }
    }
    
    public async Task<DiagnosticsRecord> GetDiagnosticsRecordByIdAsync(int id)
    {
        try
        {
            var context = new DiagnosticsContext();
            return await context.DiagnosticsRecords!.SingleAsync(d => d.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception in GetDiagnosticsRecordByIdAsync with message {e.Message}");
            throw;
        }
    }
}