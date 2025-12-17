namespace Calculator.Core.Tokens.Operators;

[Operator(Symbol = "*")]
public sealed class MultiplyOperator : BinaryOperator
{
    public override int Precedence => Precedences.Multiplication;

    protected override double Apply(double left, double right) => left * right;
}
