namespace Calculator.Core.Tokens;

public interface IToken
{
    void Apply(INumberStack stack);
}
