using GamblingSimulator.Core.Contracts;

namespace GamblingSimulator.UI
{
    internal interface ICasinoGame
    {
        public IList<ISlot> AvailableSlots { get; }
    }
}

