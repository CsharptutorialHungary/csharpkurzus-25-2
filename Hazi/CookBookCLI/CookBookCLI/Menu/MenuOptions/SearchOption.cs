using CookBookCLI.Models;
using CookBookCLI.Services;
using CookBookCLI.Storage;

namespace CookBookCLI.Menu.MenuOptions
{
    internal class SearchOption : IMenuOption
    {
        private readonly IRecipeStorage _storage;
        public string Title => "Recept keresése";

        public SearchOption(IRecipeStorage storage)
        {
            _storage = storage;
        }

        public async Task Execute()
        {
            Console.Clear();
            Console.WriteLine("=== Recept keresése ===");

            var searchDTO = Services.UserInputService.GetSearchDTOFromUserInput();
            var recipes = await _storage.LoadRecipes();
            if (recipes.Count == 0)
            {
                Console.WriteLine("Nincsen ilyen recept.");
                return;
            }

            var foundRecipes = SearchRecipes(recipes, searchDTO);
            WriteToConsoleService.WriteRecipesToConsole(foundRecipes);
        }

        private List<Recipe> SearchRecipes(List<Recipe> recipes, RecipeDTO searchDTO)
        {
            var foundRecipes = recipes
                .Where(recipe =>
                    {
                        return (string.IsNullOrWhiteSpace(searchDTO.Name) || recipe.Name.Contains(searchDTO.Name!, StringComparison.InvariantCultureIgnoreCase)) &&
                        (string.IsNullOrWhiteSpace(searchDTO.Description) || recipe.Description.Contains(searchDTO.Description!, StringComparison.InvariantCultureIgnoreCase)) &&
                        (!searchDTO.PreparationTimeInMinutes.HasValue || (recipe.PreparationTimeInMinutes - 10 <= searchDTO.PreparationTimeInMinutes.Value && recipe.PreparationTimeInMinutes + 10 >= searchDTO.PreparationTimeInMinutes.Value));
                    })
                .OrderBy(recipe => recipe.Name)
                .ToList();

            return foundRecipes;
        }
    }
}
