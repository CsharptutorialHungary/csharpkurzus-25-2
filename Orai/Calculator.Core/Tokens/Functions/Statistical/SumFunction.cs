namespace Calculator.Core.Tokens.Functions.Statistical;

[Operator(Symbol = "sum")]
internal class SumFunction : GreedyOperator
{
    protected override double Apply(IReadOnlyList<double> values) => values.Sum();
}
