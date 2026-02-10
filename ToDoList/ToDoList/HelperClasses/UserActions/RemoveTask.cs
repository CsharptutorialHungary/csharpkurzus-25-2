using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Interfaces;

namespace ToDoList.HelperClasses.UserActions
{
    internal static class RemoveTask
    {
        public static void Remove(List<ITask> tasks)
        {
            Console.Write("Enter the name of the task you want removed: ");
            string input = Console.ReadLine();

            List<ITask> shortList = tasks.FindAll(x => x.TaskName.Equals(input));

            if (shortList.Count == 0)
            {
                Console.WriteLine($"There are no tasks with the name {input}");
                return;
            }

            if (shortList.Count == 1)
            {
                Console.Clear();
                tasks.Remove(shortList[0]);
                Console.WriteLine("Task successfully removed");

            }

            else
            {
                Console.Clear();
                Console.WriteLine($"There are multiple instances named {input}");
                Console.WriteLine("Please specify");

                JsonListTasks.ListTasks(shortList, true);

                if (int.TryParse(Console.ReadLine(), out int result) && result >= 1 && result <= shortList.Count)
                {
                    Console.Clear();
                    tasks.Remove(shortList[result - 1]);
                    Console.WriteLine("Task successfully removed");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

    }
}
