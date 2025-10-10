namespace Calculator.Core.Tokens.Operators;

public abstract class Operator : IToken
{
    public abstract void Apply(INumberStack stack);

    public abstract int Precedence { get; }
    public abstract bool IsRightAssociative { get; }
}
