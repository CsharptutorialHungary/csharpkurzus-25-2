using System.Diagnostics.CodeAnalysis;

using Calculator.Core.Tokens;

namespace Calculator.Core;
internal interface ITokenRegistry
{
    bool TryGetToken(string symbol, [MaybeNullWhen(false)] out IToken token);
}