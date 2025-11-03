namespace Calculator.Core.Tokens.Functions.Statistical;

[Operator(Symbol = "rms")]
internal class RootMeanSquareFunction : GreedyOperator
{
    protected override double Apply(IReadOnlyList<double> values)
    {
        double sumOfSquares = values.Sum(value => value * value);

        return Math.Sqrt(sumOfSquares / values.Count);
    }
}
