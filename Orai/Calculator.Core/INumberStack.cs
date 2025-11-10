namespace Calculator.Core;

public interface INumberStack
{
    double Pop();

    IReadOnlyList<double> PopAll();

    void Push(double number);

    int Count { get; }
}
