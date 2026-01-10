using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookCLI.Models
{
    internal record class Ingredient
    {
        public required string Name { get; set; }
        public required string Unit { get; set; }
        public required double Quantity { get; set; }
    }
}
