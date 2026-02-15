namespace JapaneseStudyTool.JapaneseStudyTool.Core.Model
{
    public record VocabWord(string Term, string Meaning, int Mastery) : Word(Term, Meaning);
}