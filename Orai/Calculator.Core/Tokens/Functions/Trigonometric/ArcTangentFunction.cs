namespace Calculator.Core.Tokens.Functions.Trigonometric;

[Operator(Symbol = "atan")]
internal class ArcTangentFunction : FunctionOperator
{
    protected override double Apply(double value) => Math.Atan(value);
}
