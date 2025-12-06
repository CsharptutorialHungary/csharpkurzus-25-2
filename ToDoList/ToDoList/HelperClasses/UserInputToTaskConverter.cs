using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Classes;
using ToDoList.Enums;
using ToDoList.Exceptions;

namespace ToDoList.HelperClasses
{
    internal static class UserInputToTaskConverter
    {
        public static ToDoTask InputToTask(string[] input)
        {
            try
            {
                ToDoTask task = new ToDoTask(input[0], input[1], input[2]);
                return task;
            }
            catch (InvalidTaskNameException invalidName)
            {
                Console.WriteLine($"Error: {invalidName.Message}");
                throw;
            }
            catch (Exception ex) when (ex is InvalidCategoryException || ex is IndexOutOfRangeException)
            {
                if (ex is InvalidCategoryException)
                {
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    Console.WriteLine("Error: Invalid category");
                }
                
                Console.WriteLine("Choose from these categories:");

                PrintEnums.PrintEnum<TaskCategory>();
                throw;
            }
            catch (InvalidDateException invalidDate)
            {
                Console.WriteLine($"Error: {invalidDate.Message}");
                Console.WriteLine("Valid date format: 2025.10.20. 14:45");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
