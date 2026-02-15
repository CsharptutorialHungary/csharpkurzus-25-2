using JapaneseStudyTool.JapaneseStudyTool.Core.Model;
using System.Text.Json;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal sealed class VocabHandler : FileHandler
    {
        private static readonly string _vocabPath = Path.Combine(AppContext.BaseDirectory, "JapaneseStudyTool.Data", "vocab.json");

        internal static List<VocabWord> LoadVocab()
        {
            return LoadFromFile<VocabWord>(_vocabPath);
        }

        internal static void SaveVocab(List<VocabWord> newItems)
        {
            List<VocabWord> allItems = [];

            if (File.Exists(_vocabPath))
            {
                string json = File.ReadAllText(_vocabPath);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var loaded = JsonSerializer.Deserialize<List<VocabWord>>(json, jsonSerializerOptions);
                    if (loaded != null)
                    {
                        allItems = loaded;
                    }
                }
            }

            foreach (var newItem in newItems)
            {
                var index = allItems.FindIndex(item => item.Term == newItem.Term);
                if (index >= 0)
                {
                    allItems[index] = newItem;
                }
                else
                {
                    allItems.Add(newItem);
                }  
            }

            string updatedJson = JsonSerializer.Serialize(allItems, jsonSerializerOptions);
            File.WriteAllText(_vocabPath, updatedJson);
        }
    }
}