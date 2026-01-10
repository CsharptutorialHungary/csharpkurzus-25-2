using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CookBookCLI.Storage;

namespace CookBookCLI.Menu.MenuOptions
{
    internal class CreateOption : IMenuOption
    {
        private readonly IRecipeStorage _storage;
        public string Title => "Új recept hozzáadása";

        public CreateOption(IRecipeStorage storage)
        {
            _storage = storage;
        }

        public async Task Execute()
        {
            Console.Clear();
            Console.WriteLine("=== Új recept hozzáadása ===");
            var recipe = Services.UserInputService.GetRecipeFromUser();
            var storedRecipes = await _storage.LoadRecipes();
            storedRecipes.Add(recipe);
            await _storage.SaveRecipes(storedRecipes);
            Console.WriteLine("\nRecept sikeresen hozzáadva!");
        }
    }
}
