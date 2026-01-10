using CookBookCLI.Menu;
using CookBookCLI.Menu.MenuOptions;
using CookBookCLI.Models;
using CookBookCLI.Storage;
using SearchOption = CookBookCLI.Menu.MenuOptions.SearchOption;

var storage = new RecipeStorage("recipes.json");

var options = new List<IMenuOption>
{
    new ListOption(storage),
    new CreateOption(storage),
    new SearchOption(storage),
    new EditOption(storage),
};

var menu = new Menu(options);
menu.start();