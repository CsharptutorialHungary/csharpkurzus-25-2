namespace Calculator.Core.Tokens.Functions.Statistical;

[Operator(Symbol = "max")]
internal class MaxFunction : GreedyOperator
{
    protected override double Apply(IReadOnlyList<double> values) => values.Max();
}
