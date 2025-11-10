
namespace Calculator.Core;

internal class NumberStack : GenericStack<double>, INumberStack
{
    public int Count => this.AsEnumerable().Count();

    public IReadOnlyList<double> PopAll()
    {
        List<double> values = [];

        while (Count > 0)
        {
            double value = Pop();

            values.Add(value);
        }

        return values;
    }
}
