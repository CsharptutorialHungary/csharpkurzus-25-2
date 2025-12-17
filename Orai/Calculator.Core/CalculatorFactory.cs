namespace Calculator.Core;

public static class CalculatorFactory
{
    public static ICalculator Create(bool infix = false)
    {
        ITokenRegistry tokenRegistry = new TokenRegistry();
        ITokenizer tokenizer;

        if (infix)
            tokenizer = new InfixTokenizer(tokenRegistry);

        else
            tokenizer = new RpnTokenizer(tokenRegistry);

        INumberStack numberStack = new NumberStack();

        return new Calculator(tokenizer, numberStack);
    }
}
