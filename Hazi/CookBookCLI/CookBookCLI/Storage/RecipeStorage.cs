using CookBookCLI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CookBookCLI.Storage
{
    internal class RecipeStorage : IRecipeStorage
    {
        private readonly string _filePath;
        public RecipeStorage(string filePath)
        {
            _filePath = filePath;
        }

        public async Task SaveRecipes(List<Recipe> recipes)
        {
            try
            {
                await JsonStorageService.Serialize(_filePath, recipes);
            }
            catch (IOException ex) 
            { 
                Console.WriteLine($"Hiba történt a fájl elérése közben. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nem sikerült menteni a fájlt. {ex.Message}");
            }
        }

        public async Task<List<Recipe>> LoadRecipes()
        {
            try
            {
                List<Recipe>? recipes = await JsonStorageService.Deserialize<List<Recipe>>(_filePath);
                return recipes is not null ? recipes : new List<Recipe>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"A mentett fájl korrupt. {ex.Message}");
                return new List<Recipe>();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Hiba történt a fájl elérése közben. {ex.Message}");
                return new List<Recipe>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nem sikerült a recepteket betölteni. {ex.Message}");
                return new List<Recipe>();
            }
        }
    }
}
