using GamblingSimulator.Core.Models;

namespace GamblingSimulator.Core.Contracts;

public interface ISlotRepository
{
    string PlayerName { get; }

    long Balance { get; }

    List<SlotResult> History { get; }

    void RecordSlotResult(SlotResult result);
}
