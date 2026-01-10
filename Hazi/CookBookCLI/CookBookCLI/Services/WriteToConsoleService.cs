using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBookCLI.Models;

namespace CookBookCLI.Services
{
    internal static class WriteToConsoleService
    {
        /// <summary>
        /// Writed the list of recipes to the console.
        /// </summary>
        /// <param name="recipes"></param>
        public static void WriteRecipesToConsole(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("Nincsenek megjeleníthető recept.");
                return;
            }
            Console.WriteLine("---------------------------------------\n");
            for (int i = 0; i < recipes.Count; i++)
            {
                var recipe = recipes[i];
                Console.WriteLine($"{i + 1}. {recipe.Name}");
                Console.WriteLine($"Leírás: {recipe.Description}");
                Console.WriteLine($"Elkészítési idő: {recipe.PreparationTimeInMinutes} perc");
                Console.WriteLine("Hozzávalók:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"     - {ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
                }
                Console.WriteLine("\n---------------------------------------\n");
            }
        }
    }
}
