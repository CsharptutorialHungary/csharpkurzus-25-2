
using System.Globalization;

using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;

using static System.StringSplitOptions;

namespace Calculator.Core;

internal class RpnTokenizer : ITokenizer
{
    private readonly Dictionary<string, IToken> _table;

    public RpnTokenizer()
    {
        _table = new Dictionary<string, IToken>()
        {
            ["+"] = new AddOperator(),
            ["-"] = new SubtractOperator(),
            ["*"] = new MultiplyOperator(),
            ["/"] = new DivideOperator(),
            ["sin"] = new SinusFunction(),
        };
    }

    public IEnumerable<IToken> Tokenize(string expression)
    {
        foreach (string part in expression.Split(' ', RemoveEmptyEntries | TrimEntries))
        {
            if (_table.TryGetValue(part, out IToken? value))
            {
                yield return value;
            }
            else if (double.TryParse(part, CultureInfo.InvariantCulture, out double number))
            {
                yield return new NumberToken(number);
            }
            else
            {
                throw new InvalidOperationException($"Unknown token: {part}");
            }
        }
    }
}