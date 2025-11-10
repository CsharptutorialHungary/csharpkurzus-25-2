namespace Calculator.Core.Tokens;

internal abstract class GreedyOperator : Operator
{
    public override void Apply(INumberStack stack)
    {
        if (stack.Count == 0)
        {
            throw new InvalidOperationException("Not enough values on the stack.");
        }

        IReadOnlyList<double> values = stack.PopAll();

        double result = Apply(values);

        stack.Push(result);
    }

    protected abstract double Apply(IReadOnlyList<double> values);
}
