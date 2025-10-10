namespace Calculator.Core.Tokens.Operators;

public sealed class DivideOperator : BinaryOperator
{
    public override int Precedence => Precedences.Division;
    
    public override bool IsRightAssociative => false;

    protected override double Apply(double left, double right)
        => left / right;
}


