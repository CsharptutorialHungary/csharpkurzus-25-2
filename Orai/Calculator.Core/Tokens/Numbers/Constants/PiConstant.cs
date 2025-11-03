namespace Calculator.Core.Tokens.Numbers.Constants;

[Operator(Symbol = "pi")]
internal sealed class PiConstant() : NumberToken(Math.PI);
