namespace Calculator.Core.Tokens;

public abstract class Operator : IToken
{
    public abstract void Apply(INumberStack stack);
}
