using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Classes;
using ToDoList.Enums;

namespace ToDoList.HelperClasses
{
    internal static class UserIsAddingATask
    {
        public static (bool success, string[] input) GuideInput()
        {
            

            List<string> prompts = new List<string>()
            {
                "Name: ",
                "Category (number): ",
                "Deadline (yyyy.MM.dd HH:mm): "
            };

            string[] input = new string[prompts.Count()];
            string currentlyWritten = "";
            bool success = true;

            for (int i = 0; i < prompts.Count(); i++)
            {
                Console.WriteLine("Type 'exit' for leaving");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(currentlyWritten);
                Console.ResetColor();
                Console.WriteLine();

                switch (i)
                {
                    case 1:
                        PrintEnums.PrintEnum<TaskCategory>();
                        break;
                    default:
                        break;
                }

                Console.Write(prompts[i]);
                string response = Console.ReadLine();

                if (response.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    success = false;
                    break;
                }

                currentlyWritten += $"{response} ";
                input[i] = response;

                Console.Clear();
            }

            return (success,input);
        }
    }
}
