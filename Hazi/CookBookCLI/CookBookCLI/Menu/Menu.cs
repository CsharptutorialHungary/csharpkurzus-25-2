using CookBookCLI.Menu.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookCLI.Menu
{
    internal class Menu
    {
        private readonly IReadOnlyList<IMenuOption> _options;
        private readonly string _title;

        public Menu(IReadOnlyList<IMenuOption> options, string title = "==== CookBook CLI ====")
        {
            _title = title;
            _options = options;
        }

        public void start()
        {
            try 
            { 
                while (true) 
                {
                    Console.Clear();
                    Console.WriteLine(_title);

                    for (int i = 0; i < _options.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {_options[i].Title}");
                    }

                    Console.WriteLine("0. Kilépés");
                    Console.WriteLine();
                    Console.Write("Válassz egy opciót (szám): ");
                    string? choice = Console.ReadLine();

                    if (choice == "0")
                        return;
                    
                    if (int.TryParse(choice, out int choiceIndex))
                    {
                        int optionIndex = choiceIndex - 1;
                        if (optionIndex >= 0 && optionIndex < _options.Count())
                        {
                            try
                            {
                                _options[optionIndex].Execute();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Hiba történt a funkció futtatása során: {ex.Message}");
                            }
                            Console.WriteLine();
                            Console.ReadLine();
                            continue;
                        }
                    }

                    Console.WriteLine("Érvénytelen opció. Kérlek próbáld újra.");
                    Console.WriteLine("Nyomj egy entert a fojtatáshoz.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt a program futása során: {ex.Message}");
            }
        }
    }
}
