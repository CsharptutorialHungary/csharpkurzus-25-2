namespace Calculator.Core.Tokens.Numbers;

internal class NumberToken(double value) : IToken
{
    public void Apply(INumberStack stack)
    {
        stack.Push(value);
    }
}
