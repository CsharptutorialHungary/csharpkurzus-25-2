namespace quiz.Models
{
    public record QuizSubmission
    {
        public int QuizId { get; init; }
        public List<int> SelectedAnswerIndices { get; init; } = new();
    }
}
