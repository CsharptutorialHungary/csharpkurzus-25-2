using CookBookCLI.Models;

namespace CookBookCLI.Storage
{
    internal interface IRecipeStorage
    {
        /// <summary>
        /// Asynchronously loads all available recipes.
        /// </summary>
        Task<List<Recipe>> LoadRecipes();

        /// <summary>
        /// Asynchronously saves the provided list of recipes.
        /// </summary>
        /// <param name="recipes"></param>
        Task SaveRecipes(List<Recipe> recipes);
    }
}