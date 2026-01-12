using LibraryApp.Commands;
using LibraryApp.Core;
using LibraryApp.Interfaces;
using LibraryApp.Models;
using LibraryApp.Services;
using System.Reflection;

namespace LibraryApp;

public class Program
{
    public static void Main()
    {
        string filePath = "books.json";

        IFileService fileService = new JsonFileService();
        ILoggerService logger = new ConsoleLoggerService();
        var libraryService = new LibraryService(fileService, logger);

        List<Book> books = libraryService.LoadBooks(filePath);
        var commands = new List<ICommand>();


        var assembly = typeof(CommandAttribute).Assembly;

        var commandTypes = assembly.GetTypes()
            .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Where(t => t.GetCustomAttribute<CommandAttribute>() != null);

        foreach (var type in commandTypes)
        {
            var constructor = type.GetConstructors().FirstOrDefault();
            if (constructor == null) continue;

            var parameters = constructor.GetParameters();

            if (parameters.Length == 2 &&
                parameters[0].ParameterType == typeof(List<Book>) &&
                parameters[1].ParameterType == typeof(ILoggerService))
            {
                try
                {
                    var instance = (ICommand)Activator.CreateInstance(type, books, logger)!;
                    commands.Add(instance);
                    //logger.Info($"DEBUG: {type.Name} betöltve.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba a {type.Name} betöltésekor: {ex.Message}");
                }
            }
        }

        commands.Add(new SaveCommand(libraryService, books, filePath));

        commands.Add(new HelpCommand(commands));


        Console.WriteLine("Könyvtárkezelő rendszer");
        Console.WriteLine("Parancsok: " + string.Join(", ", commands.Select(GetCommandName)) + ", exit");

        bool running = true;

        while (running)
        {
            Console.Write("\n> ");
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "exit")
            {
                running = false;
                continue;
            }

            var command = commands.FirstOrDefault(c => GetCommandName(c) == input || c.Name == input);

            if (command != null)
                command.Execute();
            else
                logger.Error("Ismeretlen parancs!");
        }

        Console.WriteLine("Kilépés...");
    }

    private static string GetCommandName(ICommand command)
    {
        var attr = command.GetType().GetCustomAttribute<CommandAttribute>();
        return attr?.Symbol ?? command.Name;
    }
}