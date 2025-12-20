using GamblingSimulator.Core.Contracts;
using GamblingSimulator.Core.Models;

namespace GamblingSimulator.Core.View;

public class SlotRenderer : ISlotRenderer
{
    private const int DefaultAnimationSpeed = 50;

    public void Render(ISlot slot, SlotResult endResult, int animationLength)
    {
        Random random = new();
        string[] shownSymbols = new string[endResult.Symbols.Length];
        int stepSize = animationLength / endResult.Symbols.Length;
        for (int i = 1; i <= animationLength; i++)
        {
            for (int j = 0; j < shownSymbols.Length; j++)
            {
                var symbol =
                    (i / stepSize) < (j + 1)
                        ? slot.Symbols[random.Next(0, slot.Symbols.Count)]
                        : endResult.Symbols[j];

                shownSymbols[j] = symbol.PadLeft(2, ' ');
            }

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"[ {string.Join(" : ", shownSymbols)} ]");

            Thread.Sleep(DefaultAnimationSpeed);
        }
        Console.WriteLine();
        Console.WriteLine($"Nyeremény: {endResult.Payout:#,0} HUF");
    }
}
