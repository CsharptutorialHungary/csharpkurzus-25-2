using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace ToDoList.HelperClasses.UserActions
{
    internal static class AddTaskToList

    {
        public static void AddTask(List<ITask> JsonLista)
        {
            while (true)
            {
                var result = UserIsAddingATask.GuideInput();

                if (result.success)
                {
                    try
                    {
                        var task = UserInputToTaskConverter.InputToTask(result.input);

                        if (JsonLista.Contains(task))
                        {
                            Console.WriteLine("Task already exists");
                        }
                        else
                        {
                            JsonLista.Add(task);
                            Console.WriteLine("Task added!");
                        }

                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"There was an error while trying to add your task");
                    }
                }

                

                while (true)
                {
                    Console.Write("Add another task? (y/n): ");
                    string cont = Console.ReadLine();

                    if (cont.ToLower() == "y")
                    {
                        break;
                    }
                    else if (cont.ToLower() == "n")
                    {
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid response");
                    }
                }

                Console.Clear();
            }
        }
    }
}
