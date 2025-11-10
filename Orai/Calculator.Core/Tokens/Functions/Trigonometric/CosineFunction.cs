namespace Calculator.Core.Tokens.Functions.Trigonometric;

[Operator(Symbol = "cos")]
internal class CosineFunction : UnaryOperator
{
    protected override double Apply(double value) => Math.Cos(value);
}
