namespace Calculator.Core.Tokens.Functions.Trigonometric;

[Operator(Symbol = "atan")]
internal class ArcTangentFunction : UnaryOperator
{
    protected override double Apply(double value) => Math.Atan(value);
}
