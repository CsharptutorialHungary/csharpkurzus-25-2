namespace Calculator.Core.Tokens.Operators;

[Operator(Symbol = "*")]
public sealed class MultiplyOperator : BinaryOperator
{
    protected override double Apply(double left, double right) => left * right;
}
