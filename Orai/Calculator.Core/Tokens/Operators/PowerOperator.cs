namespace Calculator.Core.Tokens.Operators;

public sealed class PowerOperator : BinaryOperator
{
    public override int Precedence => Precedences.Power;

    public override bool IsRightAssociative => true;

    protected override double Apply(double left, double right)
    {
        return Math.Pow(left, right);
    }
}


