// ICommand.cs – minden parancs ezt az interfészt valósítja meg
namespace LibraryApp.Interfaces;

public interface ICommand
{
    string Name { get; }
    string Description { get; }
    void Execute();
}
