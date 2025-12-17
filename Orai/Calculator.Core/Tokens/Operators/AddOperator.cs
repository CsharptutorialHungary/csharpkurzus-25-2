namespace Calculator.Core.Tokens.Operators;

[Operator(Symbol = "+")]
public sealed class AddOperator : BinaryOperator
{
    public override int Precedence => Precedences.Addition;

    protected override double Apply(double left, double right) => left + right;
}
