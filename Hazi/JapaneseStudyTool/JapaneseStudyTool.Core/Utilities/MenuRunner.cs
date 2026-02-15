using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Interface;
using JapaneseStudyTool.JapaneseStudyTool.UI;
namespace JapaneseStudyTool.JapaneseStudyTool.Core.Utilitiies
{
    public static class MenuRunner
    {
        public static void RunMenu(IMenu menu)
        {
            Console.Clear();

            while (!menu.DisplayMenu()) {}
        }
    }
}
