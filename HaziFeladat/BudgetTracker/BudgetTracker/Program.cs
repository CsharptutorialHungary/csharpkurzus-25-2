using System;
using System.Collections.Generic;

namespace BudgetTracker
{
    internal class Program
    {
        static ExpenseManager manager = new ExpenseManager();

        static void Main(string[] args)
        {
            ConsoleHelper.PrintHeader("=== Budget Tracker ===");
            bool running = true;

            while (running)
            {
                ConsoleHelper.PrintHighlight("\nFOMENU:");
                Console.WriteLine("1. Uj kiadas hozzaadasa");
                Console.WriteLine("2. Kiadasok listazasa");
                Console.WriteLine("3. Statisztikak");
                Console.WriteLine("4. Kereses nev alapjan");
                Console.WriteLine("5. Kilepes");
                Console.Write("Valasztas: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            MenuAddExpense();
                            break;
                        case "2":
                            MenuListExpenses();
                            break;
                        case "3":
                            MenuShowStatistics();
                            break;
                        case "4":
                            MenuSearch();
                            break;
                        case "5":
                            running = false;
                            ConsoleHelper.PrintSuccess("Tovabbi szep napot!");
                            break;
                        default:
                            ConsoleHelper.PrintError("Ismeretlen parancs, kerlek 1-5 kozotti szamot valassz!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.PrintError($"\nError: {ex.Message}");
                }
            }
        }

        static void MenuAddExpense()
        {
            ConsoleHelper.PrintHeader("\n--- Uj kiadas ---");

            Console.Write("Megnevezes: ");
            string name = Console.ReadLine();

            Console.Write("Kategoria: ");
            string category = Console.ReadLine();

            Console.Write("Osszeg (Ft): ");
            int amount = int.Parse(Console.ReadLine());

            manager.AddExpense(name, amount, category);
            ConsoleHelper.PrintSuccess(">> Sikeresen rogzites");
        }

        static void MenuListExpenses()
        {
            var list = manager.GetAllExpenses();
            PrintList(list);
        }

        static void MenuShowStatistics()
        {
            var list = manager.GetAllExpenses();

            if (list.Count == 0)
            {
                ConsoleHelper.PrintError("Nincs eleg adat a statisztikahoz.");
                return;
            }

            int total = StatisticsService.CalculateTotal(list);

            Console.Write("\nTeljes koltes: ");
            ConsoleHelper.PrintHighlight($"{total} Ft");

            var expensive = StatisticsService.GetMostExpensiveItem(list);
            Console.Write("Legdragabb tetel: ");
            Console.Write($"{expensive.Name} ");
            ConsoleHelper.PrintHighlight($"({expensive.Amount} Ft)");

            StatisticsService.PrintCategoryBreakdown(list);
        }

        static void MenuSearch()
        {
            ConsoleHelper.PrintHeader("\n--- Kereses ---");

            Console.Write("Mit keresel?: ");
            string text = Console.ReadLine();

            var results = manager.SearchExpenses(text);

            if (results.Count > 0)
            {
                ConsoleHelper.PrintSuccess($"Talalatok szama: {results.Count}");
                PrintList(results);
            }
            else
            {
                ConsoleHelper.PrintError("Nincs talalat.");
            }
        }

        static void PrintList(List<ExpenseItem> list)
        {
            ConsoleHelper.PrintHeader($"\n--- Lista ({list.Count} tetel) ---");

            if (list.Count == 0)
            {
                Console.WriteLine("Nincs megjelenitheto adat.");
                return;
            }

            foreach (var item in list)
            {
                Console.Write($"[{item.Date.ToShortDateString()}] {item.Name} ({item.Category}): ");
                ConsoleHelper.PrintHighlight($"{item.Amount} Ft");
            }
        }
    }
}