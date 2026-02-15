using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal sealed class KanaLoader : FileHandler
    {
        private static readonly string _hiraganaPath = Path.Combine(AppContext.BaseDirectory, "JapaneseStudyTool.Data", "hiragana.json");
        private static readonly string _katakanaPath = Path.Combine(AppContext.BaseDirectory, "JapaneseStudyTool.Data", "katakana.json");

        public static KanaSet LoadKana(KanaType type)
        {
            return type switch
            {
                KanaType.Hiragana => new KanaSet { Hiragana = LoadFromFile<KanaEntry>(_hiraganaPath) },
                KanaType.Katakana => new KanaSet { Katakana = LoadFromFile<KanaEntry>(_katakanaPath) },
                KanaType.All => new KanaSet
                {
                    Hiragana = LoadFromFile<KanaEntry>(_hiraganaPath),
                    Katakana = LoadFromFile<KanaEntry>(_katakanaPath)
                },
                _ => throw new ArgumentException("File does not exist for KanaType: " + type),
            };
        }
    }
}