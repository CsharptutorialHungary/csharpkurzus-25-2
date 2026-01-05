using System.Text.Json;
using quiz.Models;
using quiz.Exceptions;

namespace quiz.Services
{
    public class QuizService
    {
        private const string FilePath = "quizzes.json";

        public List<Quiz> getQuizzesInternal()
        {
            try
            {
                if (!File.Exists(FilePath)) return new List<Quiz>();

                var json = File.ReadAllText(FilePath);
                if (string.IsNullOrWhiteSpace(json)) return new List<Quiz>();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<Quiz>>(json, options) ?? new List<Quiz>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                return new List<Quiz>();
            }
        }

        public List<QuizPublic> getQuizzesPublic()
        {
            var allQuizzes = getQuizzesInternal();
            var dtos = new List<QuizPublic>();

            foreach (var q in allQuizzes)
            {
                dtos.Add(new QuizPublic
                {
                    Id = q.Id,
                    Title = q.Title,
                    Questions = q.Questions.Select(question => new QuestionPublic
                    {
                        Text = question.Text,
                        Options = question.Options
                    }).ToList()
                });
            }

            return dtos;
        }

        public void createQuiz(Quiz quiz)
        {
            if (string.IsNullOrWhiteSpace(quiz.Title))
            {
                throw new QuizValidationException("A kvíz címét kötelező megadni!");
            }

            if (quiz.Questions == null || quiz.Questions.Count == 0)
            {
                throw new QuizValidationException("A kvíznek legalább 1 kérdést tartalmaznia kell!");
            }

            foreach (var q in quiz.Questions)
            {
                if (string.IsNullOrWhiteSpace(q.Text))
                {
                    throw new QuizValidationException("Minden kérdésnek kell, hogy legyen szövege!");
                }

                if (q.Options == null || q.Options.Count < 2)
                {
                    throw new QuizValidationException($"A '{q.Text}' kérdéshez legalább 2 válaszlehetőséget kell megadni!");
                }

                if (q.CorrectOptionIndex < 0 || q.CorrectOptionIndex >= q.Options.Count)
                {
                    throw new QuizValidationException($"A '{q.Text}' kérdésnél érvénytelen a helyes válasz indexe!");
                }
            }

            try
            {
                var quizzes = getQuizzesInternal();
                int newId = quizzes.Count > 0 ? quizzes.Max(q => q.Id) + 1 : 1;
                quiz.Id = newId;

                quizzes.Add(quiz);
                saveQuizzes(quizzes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HIBA] Nem sikerült létrehozni a kvízt: {ex.Message}");
                throw;
            }
        }

        public int evaluateQuiz(int quizId, List<int> userAnswers)
        {
            var quizzes = getQuizzesInternal();
            var quiz = quizzes.FirstOrDefault(q => q.Id == quizId);

            if (quiz == null)
            {
                throw new QuizValidationException("A kért kvíz nem található!");
            }

            if (userAnswers == null || userAnswers.Count != quiz.Questions.Count)
            {
                throw new QuizValidationException($"A kvíz {quiz.Questions.Count} kérdést tartalmaz, de te {userAnswers?.Count ?? 0} választ küldtél!");
            }

            if (userAnswers.Any(answer => answer == -1))
            {
                throw new QuizValidationException("Nem töltötted ki az összes kérdést! Kérlek válaszolj mindegyikre.");
            }

            int score = 0;
            for (int i = 0; i < quiz.Questions.Count; i++)
            {
                if (userAnswers[i] >= 0 && userAnswers[i] < quiz.Questions[i].Options.Count)
                {
                    if (userAnswers[i] == quiz.Questions[i].CorrectOptionIndex)
                    {
                        score++;
                    }
                }
            }
            return score;
        }

        private void saveQuizzes(List<Quiz> quizzes)
        {
            try
            {
                var json = JsonSerializer.Serialize(quizzes, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HIBA] Nem sikerült menteni a fájlt: {ex.Message}");
                throw;
            }
        }
    }
}
