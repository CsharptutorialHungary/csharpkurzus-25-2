using GamblingSimulator.Core;
using GamblingSimulator.Core.Contracts;
using GamblingSimulator.Core.Exceptions;
using GamblingSimulator.Core.Services;
using GamblingSimulator.Core.Storage;
using GamblingSimulator.Core.View;
using GamblingSimulator.Models;

namespace GamblingSimulator.UI
{
    internal class CasinoGame : ICasinoGame
    {
        private const int DefaultBetIncreaseAmount = 100;
        private readonly ISlotRepository _repository;
        private readonly ISlotRenderer _renderer;
        private readonly ISlotService _slotService;

        private readonly Dictionary<int, Interaction> _slotInteractions;

        private int _slotChoice;
        private int _betAmount = 200;

        public IList<ISlot> AvailableSlots { get; private set; }

        public CasinoGame()
        {
            _repository = new JsonSlotRepository("gambling_data.json");

            AvailableSlots = SlotFactory.RetriveAllSlot();
            _renderer = new SlotRenderer();
            _slotService = SlotFactory.CreateSlotService();

            _slotInteractions = new Dictionary<int, Interaction>
            {
                { 1, new Interaction("Pörgetés", SpinSlot) },
                { 2, new Interaction("Tét növelése", IncreaseBet) },
                { 3, new Interaction("Tét csökkentése", DecreaseBet) },
                { 4, new Interaction("Tét megadása", EnterBet) },
                { 5, new Interaction("Mai összegzés megtekintése", ShowTodaySummary) },
            };
        }

        public void Start()
        {
            bool isRunning = true;
            Console.WriteLine($"Elérhető slotok:");
            for (int i = 0; i < AvailableSlots.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {AvailableSlots[i].Name}");
            }
            Console.WriteLine("Kérem válasszon egy slotot a fenti listából (szám alapján):");

            var slotInput = Console.ReadLine();

            while (
                !int.TryParse(slotInput, out _slotChoice)
                || _slotChoice < 1
                || _slotChoice > AvailableSlots.Count
            )
            {
                Console.WriteLine("Érvénytelen slot, egy számot írjon be:");
                slotInput = Console.ReadLine();
            }
            Console.Clear();
            while (isRunning)
            {
                ShowPlayerState();
                foreach (var interaction in _slotInteractions)
                {
                    Console.WriteLine($"{interaction.Key}. {interaction.Value.Name}");
                }
                var input = Console.ReadLine();

                int interactionChoice;
                while (!int.TryParse(input, out interactionChoice))
                {
                    Console.WriteLine("Érvénytelen interakció, egy számot írjon be:");
                    input = Console.ReadLine();
                }
                Console.Clear();

                _slotInteractions.TryGetValue(
                    interactionChoice,
                    out Interaction? selectedInteraction
                );
                selectedInteraction?.Method();
            }
            Console.WriteLine("Viszlát! Vissza várjuk!");
        }

        private void ShowTodaySummary()
        {
            var today = DateTime.UtcNow.Date;
            var todayResults = _repository.History.Where(r => r.PlayedAt.Date == today).ToList();
            if (todayResults.Count == 0)
            {
                Console.WriteLine("Ma még nem játszottál.");
                return;
            }
            Console.WriteLine("Mai játékok:");
            foreach (var result in todayResults)
            {
                Console.WriteLine(
                    $"{result.PlayedAt:HH:mm:ss} - {result.GameName}: Tét: {result.Bet:#,0} HUF, Nyeremény: {result.Payout:#,0} HUF"
                );
            }
            long totalChange = todayResults.Sum(r => r.Payout - r.Bet);
            Console.WriteLine($"Összesen ma: {totalChange:#,0} HUF");
        }

        private void SpinSlot()
        {
            var slot = AvailableSlots[_slotChoice - 1];
            try
            {
                var result = _slotService.Spin(slot, _betAmount, _repository);
                _renderer.Render(slot, result, 30);
            }
            catch (InsufficientBalanceException)
            {
                Console.WriteLine("Nincs elég egyenleged.");
            }
        }

        private void IncreaseBet()
        {
            _betAmount += DefaultBetIncreaseAmount;
        }

        private void DecreaseBet()
        {
            if (_betAmount - DefaultBetIncreaseAmount >= 0)
            {
                _betAmount -= DefaultBetIncreaseAmount;
            }
        }

        private void EnterBet()
        {
            Console.WriteLine("Adja meg a kívánt tét összegét HUF-ban:");
            var input = Console.ReadLine();
            int enteredBet;
            while (
                !int.TryParse(input, out enteredBet)
                || enteredBet < 0
                || _repository.Balance < enteredBet
            )
            {
                Console.WriteLine("Érvénytelen tét");
                input = Console.ReadLine();
            }
            _betAmount = enteredBet;
        }

        private void ShowPlayerState()
        {
            Console.WriteLine(
                $"----- Üdv {_repository.PlayerName} ------ | Egyenleg: {_repository.Balance:#,0} HUF | {DateTime.UtcNow}"
            );
            Console.WriteLine($"Jelenlegi tét: {_betAmount:#,0} HUF");
        }
    }
}
