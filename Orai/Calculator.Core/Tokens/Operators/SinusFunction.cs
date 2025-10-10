namespace Calculator.Core.Tokens.Operators;

public sealed class SinusFunction : UnaryOperator
{
    public override int Precedence => Precedences.Function;
    public override bool IsRightAssociative => false;

    protected override double Apply(double value)
    {
        return Math.Sin(value);
    }
}
