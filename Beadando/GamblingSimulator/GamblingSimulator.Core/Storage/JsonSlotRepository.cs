namespace GamblingSimulator.Core.Storage;

using System.Text.Json;
using GamblingSimulator.Core.Contracts;
using GamblingSimulator.Core.Models;

public class JsonSlotRepository : ISlotRepository
{
    private const string DefaultHistoryPath = "gambling_data.json";
    private readonly string _filePath;
    private readonly PlayerState _state;

    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public JsonSlotRepository(string filePath = DefaultHistoryPath)
    {
        _filePath = filePath;
        _state = LoadFromFile();
    }

    public string PlayerName => _state.PlayerName;

    public long Balance => _state.Balance;

    public List<SlotResult> History => _state.History;

    public void RecordSlotResult(SlotResult result)
    {
        _state.Balance += result.Payout - result.Bet;
        _state.History.Add(result);

        SaveToFile();
    }

    private void SaveToFile()
    {
        string json = JsonSerializer.Serialize(_state, _options);
        File.WriteAllText(_filePath, json);
    }

    private PlayerState LoadFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new PlayerState();
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<PlayerState>(json, _options) ?? new PlayerState();
        }
        catch (JsonException)
        {
            return new PlayerState();
        }
    }
}
