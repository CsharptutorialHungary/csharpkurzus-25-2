using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.HelperClasses
{
    internal static class JsonFileCreator
    {
        internal static readonly string filePath = Path.Combine(AppContext.BaseDirectory, "tasks.json");

        public static void Create()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File created at {filePath}");
                File.WriteAllText(filePath, "[]");
            }
        }


    }
}
