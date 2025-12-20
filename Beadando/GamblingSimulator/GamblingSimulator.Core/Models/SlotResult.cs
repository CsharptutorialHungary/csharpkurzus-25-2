namespace GamblingSimulator.Core.Models;

public record SlotResult(
    string GameName,
    DateTime PlayedAt,
    string[] Symbols,
    long Bet,
    long Payout
);

