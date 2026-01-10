using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookCLI.Models
{
    internal record class Recipe
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int PreparationTimeInMinutes { get; set; }
        public required List<Ingredient> Ingredients { get; set; }
    }
}
