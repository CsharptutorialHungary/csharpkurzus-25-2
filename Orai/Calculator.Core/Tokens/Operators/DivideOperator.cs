namespace Calculator.Core.Tokens.Operators;

[Operator(Symbol = "/")]
public sealed class DivideOperator : BinaryOperator
{
    public override int Precedence => Precedences.Division;

    protected override double Apply(double left, double right) => left / right;
}
