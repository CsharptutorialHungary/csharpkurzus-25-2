namespace Calculator.Core.Tokens.Operators;

public sealed class AddOperator : BinaryOperator
{
    public override int Precedence => Precedences.Addition;

    public override bool IsRightAssociative => false;

    protected override double Apply(double left, double right)
        => left + right;
}


