using GamblingSimulator.Core.Contracts;
using GamblingSimulator.Core.Exceptions;
using GamblingSimulator.Core.Models;

namespace GamblingSimulator.Core.Services
{
    internal class SlotService : ISlotService
    {
        public SlotResult Spin(ISlot slot, int betAmount, ISlotRepository repository)
        {
            if (betAmount > repository.Balance)
            {
                throw new InsufficientBalanceException();
            }

            var slotResult = slot.Spin(betAmount, 20);

            repository.RecordSlotResult(slotResult);

            return slotResult;
        }
    }
}
