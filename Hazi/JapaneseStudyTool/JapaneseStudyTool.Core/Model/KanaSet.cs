namespace JapaneseStudyTool.JapaneseStudyTool.Core.Model
{
    internal sealed class KanaSet
    {
        public List<KanaEntry> Hiragana { get; init; } = [];
        public List<KanaEntry> Katakana { get; init; } = [];
    }
}
