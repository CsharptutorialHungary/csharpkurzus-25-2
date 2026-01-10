using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTracker
{
    public static class StatisticsService
    {
        // Osszes koltes
        public static int CalculateTotal(List<ExpenseItem> items)
        {
            return items.Sum(x => x.Amount);
        }

       // Legdragabb tetel meghatarozasa
        public static ExpenseItem GetMostExpensiveItem(List<ExpenseItem> items)
        {
            if (items.Count == 0) return null;
            return items.OrderByDescending(x => x.Amount).First();
        }

        // Osszesites kategoriankent
        public static void PrintCategoryBreakdown(List<ExpenseItem> items)
        {
            ConsoleHelper.PrintHeader("\n--- Kategoriankenti osszesites ---");

            var groups = items.GroupBy(x => x.Category)
                              .Select(g => new { Category = g.Key, Sum = g.Sum(i => i.Amount) })
                              .OrderByDescending(g => g.Sum);

            foreach (var group in groups)
            {
                Console.Write($"- {group.Category}: ");
                ConsoleHelper.PrintHighlight($"{group.Sum} Ft");
            }
        }
    }
}