using System.Diagnostics;

namespace RentalApp.Core.Services;

internal class DatabaseManager<TRecord> : IDatabaseManager<TRecord> where TRecord : IHasIdentifier
{

    private readonly string _databaseFile;
    private readonly IJsonParseManager _jsonParseManager;
    private IEnumerable<TRecord> _records = default!;

    public DatabaseManager(string databaseFile, IJsonParseManager jsonParseManager)
    {
        if (string.IsNullOrEmpty(databaseFile))
        {
            throw new ArgumentNullException("Please provide a valid database file path.");
        }
        _databaseFile = databaseFile;
        _jsonParseManager = jsonParseManager;
        LoadRecords();
    }

    public bool RecordExists(string identifier)
    {
        return _records.Any(record => record.Identifier == identifier);
    }

    public void AddRecord(TRecord record)
    {
        _records = _records.Concat(new List<TRecord> { record });
        SaveRecords();
    }

    public void DeleteRecord(string identifier)
    {
        DeleteRecordWoutSaving(identifier);
        SaveRecords();
    }

    private void DeleteRecordWoutSaving(string identifier)
    {
        _records = _records.Where(record => record.Identifier != identifier);   
    }

    public void UpdateRecord(TRecord record)
    {
        DeleteRecordWoutSaving(record.Identifier);
        AddRecord(record);
    }

    private void LoadRecords()
    {
        _records = _jsonParseManager.LoadRecords<TRecord>(_databaseFile);
    }

    private void SaveRecords()
    {
        _jsonParseManager.SaveRecords<TRecord>(_databaseFile, _records);
    }

}
