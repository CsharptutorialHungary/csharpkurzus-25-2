namespace Calculator.Core.Tokens.Functions.Trigonometric;

[Operator(Symbol = "acos")]
internal class ArcCosineFunction : UnaryOperator
{
    protected override double Apply(double value) => Math.Acos(value);
}
