namespace Calculator.Core.Tokens.Functions.Statistical;

[Operator(Symbol = "mode")]
internal class ModeFunction : GreedyOperator
{
    protected override double Apply(IReadOnlyList<double> values)
    {
        return values
            .GroupBy(value => value)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .First()
            .Key;
    }
}
