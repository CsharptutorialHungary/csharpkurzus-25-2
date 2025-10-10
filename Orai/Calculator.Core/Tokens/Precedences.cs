namespace Calculator.Core.Tokens;

internal static class Precedences
{
    public const int Addition = 1;
    public const int Subtraction = 1;
    public const int Multiplication = 2;
    public const int Division = 2;
    public const int Function = 3;
}
