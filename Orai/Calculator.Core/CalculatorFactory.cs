namespace Calculator.Core;

public static class CalculatorFactory
{
    public static ICalculator Create()
    {
        ITokenRegistry tokenRegistry = new TokenRegistry();

        ITokenizer tokenizer = new Tokenizer(tokenRegistry);
        INumberStack numberStack = new NumberStack();

        return new Calculator(tokenizer, numberStack);
    }
}
