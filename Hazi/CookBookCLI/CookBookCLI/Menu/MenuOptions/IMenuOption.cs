using System;
using System.Collections.Generic;
using System.Text;

namespace CookBookCLI.Menu.MenuOptions
{
    internal interface IMenuOption
    {
        string Title { get; }
        Task Execute();
    }
}
