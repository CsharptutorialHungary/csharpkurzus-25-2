namespace RentalApp.Core.Services;

internal interface IDatabaseManager<TRecord>
{
    public bool RecordExists(string identifier);

    public void AddRecord(TRecord record);

    public void DeleteRecord(string identifier);

    public void UpdateRecord(TRecord record);
}