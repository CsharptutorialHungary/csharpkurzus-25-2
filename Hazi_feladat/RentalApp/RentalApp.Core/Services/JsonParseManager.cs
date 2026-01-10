using System.Text.Json;

namespace RentalApp.Core.Services;

internal class JsonParseManager() : IJsonParseManager
{
    public Result<TRecord, string> ParseRecord<TRecord>(string jsonString)
    {
        if (string.IsNullOrEmpty(jsonString) || jsonString == "null")
        {
            return new Result<TRecord, string>("Cannot parse null or empty string.");
        }

        try
        {
            // ReflectionHelper.GetImplementingClassAsRuntimeType<TRecord>() meg kene hogy talalja az implementalo osztalyt (!)
            // JsonSerializer.Deserialize csak abban az esetben ad vissza null-t, ha maga a jsonString null, de ezt mar ellenoriztem (!)
            TRecord record = (TRecord)JsonSerializer.Deserialize(jsonString, ReflectionHelper.GetImplementingClassAsRuntimeType<TRecord>()!)!;
            Console.WriteLine($"Record parsed successfully:{Environment.NewLine}{record}");
            return new Result<TRecord, string>(record);
        }
        catch (Exception ex)
        {
            if (ex is JsonException || ex is InvalidOperationException)
            {
                return new Result<TRecord, string>(ex.Message);
            }
            throw;
        }

    }

    public void SaveRecords<TRecord>(string databaseFile, IEnumerable<TRecord> records)
    {
        string jsonString = JsonSerializer.Serialize(records, records.GetType(), new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(databaseFile, jsonString);
    }

    public IEnumerable<TRecord> LoadRecords<TRecord>(string databaseFile)
    {
        string jsonString;
        if (File.Exists(databaseFile) is false || string.IsNullOrEmpty(jsonString = File.ReadAllText(databaseFile)))
        {
            return new List<TRecord>();
        }
        return (IEnumerable<TRecord>)JsonSerializer.Deserialize(jsonString, typeof(IEnumerable<>).MakeGenericType(ReflectionHelper.GetImplementingClassAsRuntimeType<TRecord>()!))!;
    }
}
