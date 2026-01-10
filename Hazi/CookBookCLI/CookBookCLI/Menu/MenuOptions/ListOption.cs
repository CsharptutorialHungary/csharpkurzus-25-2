using CookBookCLI.Services;
using CookBookCLI.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookCLI.Menu.MenuOptions
{
    internal class ListOption : IMenuOption
    {
        private readonly IRecipeStorage _storage;
        public string Title => "Receptek listázása";

        public ListOption(IRecipeStorage storage)
        {
            _storage = storage;
        }

        public async Task Execute()
        {
            Console.Clear();
            Console.WriteLine("=== Receptek ===\n");
            var recipes = await _storage.LoadRecipes();
            
            WriteToConsoleService.WriteRecipesToConsole(recipes);
        }
    }
}
