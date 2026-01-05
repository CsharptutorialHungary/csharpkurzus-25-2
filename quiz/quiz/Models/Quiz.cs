namespace quiz.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = new();
    }

    public class Question
    {
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public int CorrectOptionIndex { get; set; }
    }

    public record QuizPublic
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public List<QuestionPublic> Questions { get; init; } = new();
    }

    public record QuestionPublic
    {
        public string Text { get; init; } = string.Empty;
        public List<string> Options { get; init; } = new();
    }
}
