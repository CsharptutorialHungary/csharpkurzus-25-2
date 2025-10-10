namespace Calculator.Core.Tokens.Operators;

public sealed class SubtractOperator : BinaryOperator
{
    public override int Precedence => Precedences.Subtraction;
    public override bool IsRightAssociative => true;

    protected override double Apply(double left, double right)
        => left - right;
}


