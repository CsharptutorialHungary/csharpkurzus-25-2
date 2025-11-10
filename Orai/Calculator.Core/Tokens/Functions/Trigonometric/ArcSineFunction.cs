namespace Calculator.Core.Tokens.Functions.Trigonometric;

[Operator(Symbol = "asin")]
internal class ArcSineFunction : UnaryOperator
{
    protected override double Apply(double value) => Math.Asin(value);
}
