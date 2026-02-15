using JapaneseStudyTool.JapaneseStudyTool.Core.Data;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;
using JapaneseStudyTool.JapaneseStudyTool.Core.Service;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class VocabQuizUI
    {
        internal static void RunVocabQuizUI()
        {
            Console.Clear();

            RandomVocabService randomVocabService = new RandomVocabService();
            List<VocabWord> vocabWords = new List<VocabWord>();
            int score = 0;
            int maxScore = randomVocabService.GetMaxScore();

            for (int i = 0; i < maxScore; i++)
            {
                VocabWord randomVocabWord = randomVocabService.GetNextWord();

                Console.WriteLine("Write the meaning of this word: " + randomVocabWord.Term);
                Console.Write(">");

                string expression = Console.ReadLine() ?? string.Empty;
                if (expression.ToLower().Trim().Equals(randomVocabWord.Meaning))
                {
                    Console.WriteLine("\nCorrect!\n");

                    randomVocabWord = randomVocabWord with { Mastery = randomVocabWord.Mastery + 1 };
                    score++;
                }
                else
                {
                    Console.WriteLine("\nIncorrect!");
                    Console.WriteLine("Correct answer: " + randomVocabWord.Meaning + "\n");

                    randomVocabWord = randomVocabWord with { Mastery = Math.Max(randomVocabWord.Mastery - 1, 0) };
                }

                vocabWords.Add(randomVocabWord);


                Console.WriteLine("Enter a key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

            VocabHandler.SaveVocab(vocabWords);
            Console.WriteLine("You finished the game with a score of " + score + "/" + maxScore + ".");

            Console.WriteLine("\nEnter a key to go back...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
