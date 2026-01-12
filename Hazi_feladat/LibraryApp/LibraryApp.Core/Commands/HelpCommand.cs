using LibraryApp.Core;
using LibraryApp.Interfaces;

namespace LibraryApp.Commands;

[Command(Symbol = "help")]
public class HelpCommand : ICommand
{
    private readonly IEnumerable<ICommand> _commands;

    public HelpCommand(IEnumerable<ICommand> commands)
    {
        _commands = commands;
    }

    public string Name => "help";
    public string Description => "Elérhető parancsok listázása és leírása.";

    public void Execute()
    {
        Console.WriteLine("\nElérhető parancsok:\n");

        foreach (var cmd in _commands.OrderBy(c => c.Name))
        {
            Console.WriteLine($"{cmd.Name.PadRight(10)} - {cmd.Description}");
        }

        Console.WriteLine("\nHasználat: írd be a parancs nevét, majd ENTER.");
    }
}
