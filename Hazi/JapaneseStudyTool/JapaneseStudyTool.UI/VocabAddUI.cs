using JapaneseStudyTool.JapaneseStudyTool.Core.Data;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class VocabAddUI
    {
        internal static void RunVocabAddUI()
        {
            List<VocabWord> words = [];

            Console.Clear();

            while (true)
            {
                Console.WriteLine("Add your desired words here. Saving occurs upon exiting.");
                Console.WriteLine("Type the word using the following format: term|meaning\n");
                Console.WriteLine("Example: りんご|apple");
                Console.WriteLine("Alternatively, type 'exit' to go back.");
                Console.Write(">");

                string expression = Console.ReadLine() ?? string.Empty;

                string[] args = expression.Split("|");
                if (args.Length == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Added " + args[0] + ".\n");

                    VocabWord word = new VocabWord(args[0], args[1], 0);
                    words.Add(word);
                }
                else if (args.Length == 1 && args[0].ToLower().Trim().Equals("exit"))
                {
                    if (words.Count > 0)
                    {
                        VocabHandler.SaveVocab(words);
                    }

                    Console.Clear();
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input.\n");
                }
            }
        }
    }
}
