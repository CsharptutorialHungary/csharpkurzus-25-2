using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Enums;
using ToDoList.Interfaces;

namespace ToDoList.HelperClasses.UserActions
{
    internal static class JsonListTasks
    {
        public static void ListTasks(IEnumerable<ITask> tasks, bool numbered = false, params TaskState[] states)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{"Task",-30} {"Category",-16} {"Deadline",-20}");
            Console.ResetColor();
            int index = 1;

            foreach (var task in tasks)
            {
                Console.ForegroundColor = task.State switch
                {
                    TaskState.Completed => ConsoleColor.Green,
                    TaskState.Pending => ConsoleColor.Yellow,
                    TaskState.Missed => ConsoleColor.Red,
                    _ => throw new ArgumentOutOfRangeException(nameof(task.State), task.State, "Unexpected task state"),
                };

                if (states.Length == 0 || states.Contains(task.State))
                {
                    if (numbered)
                    {
                        Console.WriteLine($"{index++} {task}");
                    }
                    else
                    {
                        Console.WriteLine(task);
                    }
                    
                }
            }

            Console.ResetColor();
        }

    }
}

