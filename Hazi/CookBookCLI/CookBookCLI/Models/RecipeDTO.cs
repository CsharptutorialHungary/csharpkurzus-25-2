using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookCLI.Models
{
    internal class RecipeDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? PreparationTimeInMinutes { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
    }
}
