namespace Calculator.Core.Tokens.Functions.Trigonometric;

[Operator(Symbol = "tan")]
internal class TangentFunction : UnaryOperator
{
    protected override double Apply(double value) => Math.Tan(value);
}
