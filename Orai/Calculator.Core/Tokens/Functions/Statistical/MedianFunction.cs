namespace Calculator.Core.Tokens.Functions.Statistical;

[Operator(Symbol = "median")]
internal class MedianFunction : GreedyOperator
{
    protected override double Apply(IReadOnlyList<double> values)
    {
        List<double> sortedValues = [.. values.Order()];

        int valueCount = sortedValues.Count;
        int middleIndex = valueCount / 2;

        return int.IsOddInteger(valueCount)
            ? sortedValues[middleIndex]
            : sortedValues.Skip(middleIndex - 1).Take(2).Average();
    }
}
