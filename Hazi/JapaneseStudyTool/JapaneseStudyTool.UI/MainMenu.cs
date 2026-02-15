using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Interface;
using JapaneseStudyTool.JapaneseStudyTool.Core.Utilitiies;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class MainMenu : IMenu
    {
        public bool DisplayMenu()
        {
            Console.WriteLine("Welcome to JapaneseStudyTool! Select your desired mode.");
            Console.WriteLine("[1] Kana practice");
            Console.WriteLine("[2] Vocabulary practice");
            Console.WriteLine("[3] Exit");

            Console.Write(">");
            string expression = Console.ReadLine() ?? string.Empty;

            return EvaluateUserExpression(expression);
        }

        internal bool EvaluateUserExpression(string expression)
        {
            if (Int32.TryParse(expression, out int modeInt) && Enum.IsDefined(typeof(MainMenuMode), modeInt))
            {
                MainMenuMode mode = (MainMenuMode)modeInt;

                switch (mode)
                {
                    case MainMenuMode.KanaPractice:
                        MenuRunner.RunMenu(new KanaMenu());
                        break;

                    case MainMenuMode.VocabularyPractice:
                        MenuRunner.RunMenu(new VocabMenu());
                        break;

                    case MainMenuMode.Exit:
                        Environment.Exit(0);
                        return true;
                }
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
