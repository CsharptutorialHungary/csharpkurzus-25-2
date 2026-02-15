using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Interface;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class KanaMenu : IMenu
    {
        public bool DisplayMenu()
        {
            Console.WriteLine("Welcome to Kana practice!");
            Console.WriteLine("With this module, you can practice reading hiragana, katakana and both!\n");

            Console.WriteLine("To start, enter the desired difficulty and the mode using the following keywords:");
            Console.WriteLine(DifficultyLevel.Easy + " | " + DifficultyLevel.Medium + " | " + DifficultyLevel.Hard);
            Console.WriteLine(KanaType.Hiragana + " | " + KanaType.Katakana + " | " + KanaType.All + "\n");

            Console.WriteLine("Example: 'easy hiragana'");
            Console.WriteLine("Alternatively, type 'exit' to go back!");

            Console.Write(">");
            string expression = Console.ReadLine() ?? string.Empty;

            return EvaluateUserExpression(expression);
        }

        internal bool EvaluateUserExpression(string expression)
        {
            string[] args = expression.Split(" ");

            if (args.Length == 2)
            {
                if (Enum.TryParse<DifficultyLevel>(args[0], ignoreCase: true, out var difficultyLevel) &&
                    Enum.TryParse<KanaType>(args[1], ignoreCase: true, out var kanaType))
                {
                    KanaPracticeUI.RunKanaPracticeUI(difficultyLevel, kanaType);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input.\n");
                }
            }
            else if (args.Length == 1 && args[0].ToLower().Equals("exit"))
            {
                Console.Clear();
                return true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input.\n");
            }

            return false;
        }
    }
}
