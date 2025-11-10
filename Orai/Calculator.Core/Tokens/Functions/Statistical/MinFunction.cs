namespace Calculator.Core.Tokens.Functions.Statistical;

[Operator(Symbol = "min")]
internal class MinFunction : GreedyOperator
{
    protected override double Apply(IReadOnlyList<double> values) => values.Min();
}
