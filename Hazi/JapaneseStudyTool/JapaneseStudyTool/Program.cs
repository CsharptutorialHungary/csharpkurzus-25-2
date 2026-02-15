using JapaneseStudyTool.JapaneseStudyTool.Core.Utilitiies;
using JapaneseStudyTool.JapaneseStudyTool.UI;
using System.Text;

internal class Program
{
    private static void Main()
    {
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;

        MenuRunner.RunMenu(new MainMenu());
    }
}