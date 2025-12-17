namespace Calculator.Core.Tokens.Operators;

[Operator(Symbol = "-")]
public sealed class SubtractOperator : BinaryOperator
{
    public override int Precedence => Precedences.Subtraction;

    protected override double Apply(double left, double right) => left - right;
}
