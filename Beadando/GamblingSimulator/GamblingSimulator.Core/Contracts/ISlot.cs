using GamblingSimulator.Core.Models;

namespace GamblingSimulator.Core.Contracts;

public interface ISlot
{
    string Name { get; }
    List<string> Symbols { get; }
    SlotResult Spin(long bet, int length);
}
