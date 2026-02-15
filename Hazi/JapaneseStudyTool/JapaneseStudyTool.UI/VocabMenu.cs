using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Interface;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class VocabMenu : IMenu
    {
        public bool DisplayMenu()
        {
            Console.WriteLine("Welcome to Vocabulary practice!");
            Console.WriteLine("With this module, you can enter words and quiz yourself!\n");
            Console.WriteLine("[1] Quiz");
            Console.WriteLine("[2] Enter words");
            Console.WriteLine("[3] Exit");

            Console.Write(">");
            string expression = Console.ReadLine() ?? string.Empty;

            return EvaluateUserExpression(expression);
        }

        internal bool EvaluateUserExpression(string expression)
        {
            if (Int32.TryParse(expression, out int modeInt) && Enum.IsDefined(typeof(VocabMenuMode), modeInt))
            {
                VocabMenuMode mode = (VocabMenuMode)modeInt;

                switch (mode)
                {
                    case VocabMenuMode.Quiz:
                        try
                        {
                            VocabQuizUI.RunVocabQuizUI();
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine("You don't have anything in your vocabulary.\n");
                        }
                        break;

                    case VocabMenuMode.AddWords:
                        VocabAddUI.RunVocabAddUI();
                        break;

                    case VocabMenuMode.Exit:
                        Console.Clear();
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
