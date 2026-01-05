using Microsoft.AspNetCore.Mvc;
using quiz.Models;
using quiz.Services;
using quiz.Exceptions;

namespace quiz.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("create")]
        public IActionResult createQuiz([FromBody] Quiz quiz)
        {
            try
            {
                _quizService.createQuiz(quiz);
                return Ok($"Kvíz sikeresen létrehozva! ID: {quiz.Id}");
            }
            catch (QuizValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Rendszerhiba történt: {ex.Message}");
            }
        }

        [HttpGet("list")]
        public ActionResult<List<QuizPublic>> ListQuizzes()
        {
            try
            {
                return _quizService.getQuizzesPublic();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a listázáskor: {ex.Message}");
            }
        }

        [HttpPost("submit")]
        public IActionResult submitQuiz([FromBody] QuizSubmission submission)
        {
            try
            {
                var allQuizzes = _quizService.getQuizzesInternal();

                int score = _quizService.evaluateQuiz(submission.QuizId, submission.SelectedAnswerIndices);

                var quiz = allQuizzes.FirstOrDefault(q => q.Id == submission.QuizId);
                int total = quiz?.Questions.Count ?? 0;

                return Ok(new
                {
                    QuizTitle = quiz?.Title ?? "Ismeretlen",
                    Score = score,
                    TotalQuestions = total
                });
            }
            catch (QuizValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba a kiértékeléskor: {ex.Message}");
            }
        }
    }
}
