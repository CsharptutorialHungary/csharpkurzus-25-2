using NUnit.Framework;
using quiz.Services;
using quiz.Models;
using quiz.Exceptions;
using System.Collections.Generic;

namespace quiz.Tests
{
    public class QuizServiceTests
    {
        private QuizService _quizService;

        [SetUp]
        public void Setup()
        {
            _quizService = new QuizService();
        }

        [Test]
        public void CreateQuiz_TitleIsEmpty_ThrowsQuizValidationException()
        {
            var invalidQuiz = new Quiz
            {
                Title = "",
                Questions = new List<Question>
                {
                    new Question { Text = "Q1", Options = new List<string> { "A", "B" }, CorrectOptionIndex = 0 }
                }
            };

            var ex = Assert.Throws<QuizValidationException>(() => _quizService.createQuiz(invalidQuiz));
            Assert.That(ex.Message, Is.EqualTo("A kvíz címét kötelezõ megadni!"));
        }

        [Test]
        public void CreateQuiz_QuestionListIsEmpty_ThrowsQuizValidationException()
        {
            var invalidQuiz = new Quiz
            {
                Title = "Valid Cím",
                Questions = new List<Question>()
            };

            Assert.Throws<QuizValidationException>(() => _quizService.createQuiz(invalidQuiz));
        }

        [Test]
        public void CreateQuiz_QuestionHasLessThanTwoOptions_ThrowsQuizValidationException()
        {
            var invalidQuiz = new Quiz
            {
                Title = "Valid Cím",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Text = "Kérdés?",
                        Options = new List<string> { "Csak egy válasz" },
                        CorrectOptionIndex = 0
                    }
                }
            };

            var ex = Assert.Throws<QuizValidationException>(() => _quizService.createQuiz(invalidQuiz));
            Assert.That(ex.Message, Does.Contain("legalább 2 válaszlehetõséget"));
        }

        [Test]
        public void CreateQuiz_CorrectOptionIndexIsOutOfRange_ThrowsQuizValidationException()
        {
            var invalidQuiz = new Quiz
            {
                Title = "Valid Cím",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Text = "Kérdés?",
                        Options = new List<string> { "A", "B" },
                        CorrectOptionIndex = 5
                    }
                }
            };

            Assert.Throws<QuizValidationException>(() => _quizService.createQuiz(invalidQuiz));
        }

        [Test]
        public void EvaluateQuiz_QuizIdDoesNotExist_ThrowsQuizValidationException()
        {
            int nonExistentId = 9999;
            var answers = new List<int> { 0 };

            var ex = Assert.Throws<QuizValidationException>(() => _quizService.evaluateQuiz(nonExistentId, answers));

            Assert.That(ex.Message, Is.EqualTo("A kért kvíz nem található!"));
        }
    }
}