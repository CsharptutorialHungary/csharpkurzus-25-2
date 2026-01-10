using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookCLI.Models
{
    internal class IngredientDTO
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public double? Quantity { get; set; }
    }
}
