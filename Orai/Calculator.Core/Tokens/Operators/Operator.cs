namespace Calculator.Core.Tokens.Operators;

public abstract class Operator : IToken
{
    public abstract void Apply(INumberStack stack);

    public abstract int Precedence { get; }

    /// <summary>
    /// Associativity.
    /// a - b - c is (a - b) - c, so subtraction is left associative.
    /// a + b + c is (a + b) + c, so addition is left associative.
    /// a * b * c is (a * b) * c, so multiplication is left associative.
    /// a / b / c is (a / b) / c, so division is left associative.
    /// a ^ b ^ c is a ^ (b ^ c), so exponentiation is right associative.
    /// </summary>
    public abstract bool IsRightAssociative { get; }
}
