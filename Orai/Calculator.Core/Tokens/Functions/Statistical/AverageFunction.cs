namespace Calculator.Core.Tokens.Functions.Statistical;

[Operator(Symbol = "avg")]
internal class AverageFunction : GreedyOperator
{
    protected override double Apply(IReadOnlyList<double> values) => values.Average();
}
