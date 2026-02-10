using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.Interfaces;

namespace ToDoList.HelperClasses
{
    internal static class JsonSaveListToFile
    {

        public static void SaveListToFile(IEnumerable<ITask> tasks)
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(JsonFileCreator.filePath, json);
        }
    }
}
