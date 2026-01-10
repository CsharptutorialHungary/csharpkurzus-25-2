namespace RentalApp.Core.Services;

internal interface IJsonParseManager
{

    Result<TRecord, string> ParseRecord<TRecord>(string jsonString);
    void SaveRecords<TRecord>(string databaseFile, IEnumerable<TRecord> records);
    IEnumerable<TRecord> LoadRecords<TRecord>(string databaseFile);
    
}