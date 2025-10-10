namespace Calculator.Core.Tokens.Operators;

public sealed class MultiplyOperator : BinaryOperator
{
    public override int Precedence => Precedences.Multiplication;
    public override bool IsRightAssociative => false;

    protected override double Apply(double left, double right)
        => left * right;
}


