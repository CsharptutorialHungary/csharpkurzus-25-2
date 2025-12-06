using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Enums;
using ToDoList.Interfaces;

namespace ToDoList.HelperClasses.UserActions
{
    internal static class TaskStateUpdate
    {
        public static void UpdateTaskState(List<ITask> tasks)
        {
            Console.Write("Enter the name of the task you want its status changed: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                
                return;
            }
            PrintEnums.PrintEnum<TaskState>();

            Console.Write("Please enter the new state you want: ");

            if (!Enum.TryParse<TaskState>(Console.ReadLine(), out TaskState newState) || !Enum.IsDefined(typeof(TaskState),newState))
            {
                Console.WriteLine("Invalid state!");
                return;
            }

            List<ITask> shortList = tasks.FindAll(x => x.TaskName.Equals(input) && x.State != newState);

            ITask chosenTask = null;

            if (shortList.Count == 0)
            {
                Console.WriteLine($"There are no tasks with name {input}");
                return;
            }

            if (shortList.Count == 1)
            {
                chosenTask = shortList[0];
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"There are multiple instances named {input}");
                Console.WriteLine("Please specify");

                JsonListTasks.ListTasks(shortList,true);

                if (int.TryParse(Console.ReadLine(), out int result) && result >= 1 && result <= shortList.Count)
                {
                    chosenTask = shortList[result - 1];
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }

            }
            if (chosenTask is not null)
            {
                Console.Clear();
                chosenTask.State = newState;
                Console.WriteLine($"Task successfully marked as {newState}!");
            }
        }
    }
}
