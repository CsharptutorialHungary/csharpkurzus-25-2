using GamblingSimulator.Core.Models;

namespace GamblingSimulator.Core.Contracts;

public interface ISlotRenderer
{
    void Render(ISlot slot, SlotResult endResult, int animationLength);
}
