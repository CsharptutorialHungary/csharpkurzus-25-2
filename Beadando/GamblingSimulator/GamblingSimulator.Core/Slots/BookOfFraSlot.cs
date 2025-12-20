using GamblingSimulator.Core.Contracts;
using GamblingSimulator.Core.Models;

namespace GamblingSimulator.Core.Slots;

internal class BookOfFraSlot : ISlot
{
    private readonly Dictionary<string, long> _symbols = new()
    {
        { "🔟", 10 },
        { "K", 10 },
        { "Q", 10 },
        { "J", 10 },
        { "A", 10 },
        { "🐞", 30 },
        { "🗿", 30 },
        { "🗽", 50 },
        { "🤠", 100 },
    };

    private readonly Dictionary<int, long> _multipliers = new()
    {
        { 1, 0 },
        { 2, 5 },
        { 3, 50 },
    };

    public string Name
    {
        get => "Book of Fra";
    }

    public List<string> Symbols
    {
        get => [.. _symbols.Keys];
    }

    public SlotResult Spin(long bet, int length)
    {
        string[] row = [RandomSymbol(), RandomSymbol(), RandomSymbol()];
        var payout = CalculatePayout(bet, row);

        return new SlotResult(Name, DateTime.Now, row, bet, payout);
    }

    private long CalculatePayout(long bet, string[] row)
    {
        var winnerSymbol = _symbols
            .Select(
                (x) => new KeyValuePair<string, int>(x.Key, row.Count(symbol => symbol == x.Key))
            )
            .FirstOrDefault(x => x.Value >= 2);

        if (winnerSymbol.Key == null)
        {
            return 0;
        }
        return _symbols[winnerSymbol.Key] * _multipliers[winnerSymbol.Value] * bet;
    }

    private string RandomSymbol()
    {
        var random = new Random();
        return _symbols.ElementAt(random.Next(0, _symbols.Count)).Key;
    }
}
