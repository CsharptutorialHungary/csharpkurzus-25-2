using CookBookCLI.Models;
using CookBookCLI.Services;
using CookBookCLI.Storage;

namespace CookBookCLI.Menu.MenuOptions
{
    internal class EditOption : IMenuOption
    {
        private readonly IRecipeStorage _storage;
        public string Title => "Recept szerkesztése";

        public EditOption(IRecipeStorage storage)
        {
            _storage = storage;
        }

        public async Task Execute()
        {
            Console.Clear();
            Console.WriteLine("=== Recept szerkesztése ===\n");

            var recipes = await _storage.LoadRecipes();
            WriteToConsoleService.WriteRecipesToConsole(recipes);

            int recipeIndex = UserInputService.GetRecipeIndexFromUser(recipes.Count);

            var editedRecipe = GetEditedRecipe(recipes[recipeIndex]);

            recipes[recipeIndex] = editedRecipe;
            await _storage.SaveRecipes(recipes);
            Console.Clear();
            Console.WriteLine("\nA recept sikeresen szerkesztve!");
        }

        private Recipe GetEditedRecipe(Recipe originalRecipe)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Recept szerkesztése ===\n");

                Console.WriteLine("1. Recept címe");
                Console.WriteLine("2. Recept leírása");
                Console.WriteLine("3. Recept elkészítési ideje");
                Console.WriteLine("4. Hozzávalók");
                Console.WriteLine("0. Kész");

                Console.Write("Írja be a számot amit szerkeszteni szeretne: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Új cím: ");
                        var newName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newName))
                        {
                            originalRecipe = originalRecipe with { Name = newName };
                            continue;
                        }
                        break;
                    case "2":
                        Console.Write("Új leírás: ");
                        var newDescription = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newDescription))
                        {
                            originalRecipe = originalRecipe with { Description = newDescription };
                            continue;
                        }
                        break;
                    case "3":
                        Console.Write("Új elkészítési idő (perc): ");
                        if (int.TryParse(Console.ReadLine(), out int newTime))
                        {
                            originalRecipe = originalRecipe with { PreparationTimeInMinutes = newTime };
                        }
                        break;
                    case "4":
                        originalRecipe = originalRecipe with { Ingredients = EditIngredients(originalRecipe.Ingredients) };
                        break;
                    case "0":
                        return originalRecipe;
                    default:
                        Console.WriteLine("Érvénytelen választás. Kérem próbálja újra.");
                        break;
                }
            }
        }
        
        private List<Ingredient> EditIngredients(List<Ingredient> originalIngredients)
        {
            var ingredients = new List<Ingredient>(originalIngredients);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Hozzávalók szerkesztése ===\n");
                for (int i = 0; i < ingredients.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ingredients[i].Name} - {ingredients[i].Quantity} {ingredients[i].Unit}");
                }
                Console.WriteLine("H. Hozzáadás");
                Console.WriteLine("T. Törlés");
                Console.WriteLine("K. Kész");
                Console.Write("Írja be a művelet számát: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "H":
                        var newIngredient = UserInputService.GetIngredientFromUser();
                        ingredients.Add(newIngredient);
                        break;
                    case "T":
                        Console.Write("Adja meg a törölni kívánt hozzávaló számát: ");
                        if (int.TryParse(Console.ReadLine(), out int ingredientToRemoveIndex) && ingredientToRemoveIndex > 0 && ingredientToRemoveIndex <= ingredients.Count)
                        {
                            ingredients.RemoveAt(ingredientToRemoveIndex - 1);
                        }
                        break;
                    case "K":
                        return ingredients;
                    default:
                        Console.WriteLine("Érvénytelen választás. Kérem próbálja újra.");
                        break;
                }
            }
        }
    }
}
