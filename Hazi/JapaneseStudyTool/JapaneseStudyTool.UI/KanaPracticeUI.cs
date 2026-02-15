using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;
using JapaneseStudyTool.JapaneseStudyTool.Core.Service;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class KanaPracticeUI
    {
        private static readonly int _maxScore = 10;

        internal static void RunKanaPracticeUI(DifficultyLevel difficultyLevel, KanaType kanaType)
        {
            Console.Clear();

            RandomKanaService randomKanaService = new RandomKanaService(difficultyLevel, kanaType);
            int score = 0;

            for (int i = 0; i < _maxScore; i++)
            {
                Word randomKanaWord = randomKanaService.GenerateRandomWord();

                Console.WriteLine("Write the meaning of this kana: " + randomKanaWord.Term);
                Console.Write(">");

                string expression = Console.ReadLine() ?? string.Empty;
                if(expression.ToLower().Trim().Equals(randomKanaWord.Meaning))
                {
                    Console.WriteLine("\nCorrect!\n");
                    score++;
                }
                else
                {
                    Console.WriteLine("\nIncorrect!");
                    Console.WriteLine("Correct answer: " + randomKanaWord.Meaning + "\n");
                }

                Console.WriteLine("Enter a key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("You finished the game with a score of " + score + "/" + _maxScore + ".");

            Console.WriteLine("\nEnter a key to go back...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
