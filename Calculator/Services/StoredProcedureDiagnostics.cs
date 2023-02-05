using System.Data;
using Calculator.Data;
using Calculator.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Calculator.Services;

public class StoredProcedureDiagnostics : IDiagnostics
{
    private readonly string _connectionString;
    private readonly SqlConnection _connection;

    public StoredProcedureDiagnostics(string connectionString)
    {
        _connectionString = connectionString;
        _connection = new SqlConnection(_connectionString);
    }

    public void LogResult(string methodName, int result)
    {
        try
        {
            _connection.Open();

            var command = new SqlCommand("AddDiagnosticsRecord", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@MethodName", methodName);
            command.Parameters.AddWithValue("@Result", result);
            command.Parameters.AddWithValue("@Timestamp", DateTime.Now);

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception in LogResult with message {e.Message}");
            throw;
        }
    }
    
    public List<DiagnosticsRecord> GetDiagnosticsRecords()
    {
        try
        {
            var records = new List<DiagnosticsRecord>();

            _connection.Open();

            var command = new SqlCommand("GetDiagnosticsRecords", _connection);
            command.CommandType = CommandType.StoredProcedure;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var record = new DiagnosticsRecord
                {
                    MethodName = reader["MethodName"].ToString(),
                    Result = int.Parse(reader["Result"].ToString()!),
                    Timestamp = DateTime.Parse(reader["Timestamp"].ToString()!)
                };

                records.Add(record);
            }

            return records;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception in GetDiagnosticsRecords with message {e.Message}");
            throw;
        }
    }
}