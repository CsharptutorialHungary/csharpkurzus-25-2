using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BudgetTracker
{
    public class FileService
    {
        private const string FileName = "expenses.json";

        public void SaveData(List<ExpenseItem> data)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(data, options);
                File.WriteAllText(FileName, jsonString);
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError($"[Fajl Hiba] Sikertelen mentes: {ex.Message}");
            }
        }

        public List<ExpenseItem> LoadData()
        {
            if (!File.Exists(FileName))
            {
                return new List<ExpenseItem>();
            }

            try
            {
                string jsonString = File.ReadAllText(FileName);
                var data = JsonSerializer.Deserialize<List<ExpenseItem>>(jsonString);
                return data ?? new List<ExpenseItem>();
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintError($"[Fajl Hiba] Sikertelen betoltes: {ex.Message}");
                return new List<ExpenseItem>();
            }
        }
    }
}