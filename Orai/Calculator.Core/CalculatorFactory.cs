namespace Calculator.Core;

public static class CalculatorFactory
{
    public static ICalculator Create(bool infix = false)
    {
        ITokenizer tokenizer = infix ? new InfixTokenizer() : new RpnTokenizer();
        INumberStack numberStack = new NumberStack();

        return new Calculator(tokenizer, numberStack);
    }
}
