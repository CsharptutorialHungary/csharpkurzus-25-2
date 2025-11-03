
using System.Globalization;

using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Numbers;

using static System.StringSplitOptions;

namespace Calculator.Core;

internal class Tokenizer(ITokenRegistry registry) : ITokenizer
{
    public IEnumerable<IToken> Tokenize(string expression)
    {
        foreach (string part in expression.Split(' ', RemoveEmptyEntries | TrimEntries))
        {
            if (registry.TryGetToken(part, out IToken? token))
            {
                yield return token;
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