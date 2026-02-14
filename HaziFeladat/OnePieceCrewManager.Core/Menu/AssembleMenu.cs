using OnePieceCrewManager.Core.Exceptions;

public class AssembleMenu
{
    private readonly Dictionary<string, IMenuAction> _actions;

    public AssembleMenu(IEnumerable<IMenuAction> actions)
    {
        _actions = actions.ToDictionary(a => a.Key);
    }

    public void Run()
    {
        Console.WriteLine("< One Piece Crew Manager >\n");

        bool exit = false;
        while (!exit)
        {
            foreach (var menuAction in _actions.Values)
                Console.WriteLine($"{menuAction.Key}) {menuAction.Description}");
            Console.WriteLine("0) Kilépés");
            Console.Write("> ");

            var choice = Console.ReadLine();
            if (choice == "0") { exit = true; continue; }

            if (_actions.TryGetValue(choice, out IMenuAction? action))
            {
                try { action.Execute(); }
                catch (CrewException ex) { Console.WriteLine($"Üzleti hiba: {ex.Message}"); }
                catch (Exception ex) { Console.WriteLine($"Váratlan hiba: {ex.Message}"); }
            }
            else
            {
                Console.WriteLine("Ismeretlen választás.");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Kilépés. Viszlát, kalóz!");
    }
}
