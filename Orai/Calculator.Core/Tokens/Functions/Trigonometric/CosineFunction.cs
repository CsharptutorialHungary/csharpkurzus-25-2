namespace Calculator.Core.Tokens.Functions.Trigonometric;

[Operator(Symbol = "cos")]
internal class CosineFunction : FunctionOperator
{
    protected override double Apply(double value) => Math.Cos(value);
}
