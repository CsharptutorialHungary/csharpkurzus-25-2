namespace Calculator.Core.Tokens;

[AttributeUsage(AttributeTargets.Class)]
internal class OperatorAttribute : Attribute
{
    public required string Symbol { get; init; }
}
