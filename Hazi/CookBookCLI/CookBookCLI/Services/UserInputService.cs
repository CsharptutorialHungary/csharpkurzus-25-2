using CookBookCLI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookCLI.Services
{
    internal static class UserInputService
    {

        /// <summary>
        /// Uses the console to get recipe data from the user. Returns a Recipe object using the data.
        /// </summary>
        public static Recipe GetRecipeFromUser()
        {
            var recipeDTO = new RecipeDTO();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(recipeDTO.Name))
                {
                    Console.Write("Add meg a recept címét: ");
                    recipeDTO.Name = Console.ReadLine();
                }
                else if (string.IsNullOrWhiteSpace(recipeDTO.Description))
                {
                    Console.Write("Add meg a recept leírását: ");
                    recipeDTO.Description = Console.ReadLine();
                }
                else if (recipeDTO.PreparationTimeInMinutes is null)
                {
                    Console.Write("Add meg, hogy mennyi idő elkészíteni a receptet (perc): ");
                    try
                    {
                        int result = int.Parse(Console.ReadLine()!);
                        recipeDTO.PreparationTimeInMinutes = result;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Kérlek számot adj meg!");
                        continue;
                    }
                }
                else if (recipeDTO.Ingredients is null || recipeDTO.Ingredients.Count == 0)
                {
                    recipeDTO.Ingredients = GetIngredientListFromUser();
                }
                else 
                {
                    break;
                }
            }

            return new Recipe
            {
                Name = recipeDTO.Name!,
                Description = recipeDTO.Description!,
                PreparationTimeInMinutes = recipeDTO.PreparationTimeInMinutes!.Value,
                Ingredients = recipeDTO.Ingredients!
            };
        }

        /// <summary>
        /// Uses GetIngredientFromUser in a loop to get a list of ingredients from the user.
        /// </summary>
        public static List<Ingredient> GetIngredientListFromUser()
        {
            List<Ingredient> ingredients = new();
            while (true) 
            {
                Console.WriteLine();
                Console.Write("Szeretnél újabb hozzávalót hozzáadni? (I/n): ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "I":
                    case "i":
                    case "":
                        var ingredient = GetIngredientFromUser();
                        ingredients.Add(ingredient);
                        break;
                    case "N":
                    case "n":
                        if (ingredients.Count == 0)
                        {
                            Console.WriteLine("Legalább egy hozzávalót meg kell adnod!");
                            continue;
                        }
                        else
                        {
                            return ingredients;
                        }
                    default:
                        Console.WriteLine("Érvénytelen választás! Kérlek, válaszolj 'I' vagy 'n' betűvel.");
                        break;
                }
            }
        }

        /// <summary>
        /// Uses the console to get ingredient data from the user. Returns an Ingredient object using the data.
        /// </summary>
        public static Ingredient GetIngredientFromUser()
        {
            IngredientDTO ingredientDTO = new();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(ingredientDTO.Name))
                {
                    Console.Write("Add meg a hozzávaló nevét: ");
                    ingredientDTO.Name = Console.ReadLine();
                }
                else if (ingredientDTO.Quantity is null)
                {
                    Console.Write("Add meg a hozzávaló mennyiségét: ");
                    try
                    {
                        double result = double.Parse(Console.ReadLine()!);
                        ingredientDTO.Quantity = result;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Kérlek számot adj meg!");
                        continue;
                    }
                }
                else if (string.IsNullOrWhiteSpace(ingredientDTO.Unit))
                {
                    Console.Write("Add meg a hozzávaló mértékegységét: ");
                    ingredientDTO.Unit = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            return new Ingredient
            {
                Name = ingredientDTO.Name!,
                Quantity = ingredientDTO.Quantity!.Value,
                Unit = ingredientDTO.Unit!
            };
        }

        /// <summary>
        /// Returns a RecipeDTO object based on user input for searching recipes.
        /// </summary>
        public static RecipeDTO GetSearchDTOFromUserInput()
        {
            var recipeDTO = new RecipeDTO();

            Console.WriteLine("Keresési feltételek megadása (üres mező = nincs megkötés):");
            Console.Write("Add meg a recept címét: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) 
            {
                recipeDTO.Name = name;
            }

            Console.Write("Add meg a recept leírását: ");
            var description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                recipeDTO.Description = description;
            }

            Console.Write("Add meg, hogy mennyi idő elkészíteni a receptet (perc): ");
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        break;
                    }
                    int result = int.Parse(input);
                    recipeDTO.PreparationTimeInMinutes = result;
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Kérlek számot adj meg!");
                    continue;
                }
            }

            return recipeDTO;
        }

        /// <summary>
        /// Gets the wanted index from the user, using the console.
        /// </summary>
        /// <param name="recipesCount"></param>
        public static int GetRecipeIndexFromUser(int recipesCount)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Adja meg a szerkeszteni kívánt recept számát (1 - {recipesCount}): ");
                    string? input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Kérem adjon meg egy számot.");
                        continue;
                    }

                    int recipeIndex = int.Parse(input) - 1;

                    if (!(recipeIndex >= 0 && recipeIndex < recipesCount))
                    {
                        Console.WriteLine("Érvénytelen szám. Próbálja újra.");
                        continue;
                    }

                    return recipeIndex;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Kérem számot adjon meg.");
                    continue;
                }
            }
        }
    }
}
