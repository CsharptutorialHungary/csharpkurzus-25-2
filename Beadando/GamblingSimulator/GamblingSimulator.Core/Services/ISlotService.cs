using GamblingSimulator.Core.Contracts;
using GamblingSimulator.Core.Models;

namespace GamblingSimulator.Core.Services
{
    public interface ISlotService
    {
        SlotResult Spin(ISlot slot, int betAmount, ISlotRepository slotRepository);
    }
}
