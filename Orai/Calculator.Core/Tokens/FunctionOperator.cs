namespace Calculator.Core.Tokens;

public abstract class FunctionOperator : UnaryOperator
{
    public override int Precedence => Precedences.Function;
}
