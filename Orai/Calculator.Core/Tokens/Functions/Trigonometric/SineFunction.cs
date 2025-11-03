namespace Calculator.Core.Tokens.Functions.Trigonometric;

[Operator(Symbol = "sin")]
public sealed class SineFunction : UnaryOperator
{
    protected override double Apply(double value) => Math.Sin(value);
}
