using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToDoList.Classes;


namespace ToDoList.HelperClasses
{
    internal static class JsonLoadFileToList
    {
        public static List<ToDoTask> LoadList()
        {
            string jsonString = File.ReadAllText(JsonFileCreator.filePath);

            return JsonSerializer.Deserialize<List<ToDoTask>>(jsonString) ?? throw new JsonException("File not created or damaged");

             
        }

    }
}
