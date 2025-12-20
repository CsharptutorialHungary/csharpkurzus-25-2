using GamblingSimulator.Core.Contracts;
using GamblingSimulator.Core.Services;
using GamblingSimulator.Core.Slots;

namespace GamblingSimulator.Core
{
    public static class SlotFactory
    {
        public static IList<ISlot> RetriveAllSlot()
        {
            IList<ISlot> slots = new List<ISlot> { new BookOfFraSlot() };

            return slots;
        }

        public static ISlotService CreateSlotService()
        {
            return new SlotService();
        }
    }
}
