using System.Diagnostics.CodeAnalysis;

using Calculator.Core.Tokens;

namespace Calculator.Core;
internal interface ITokenRegistry
{
    IEnumerable<string> KnownTokenSymbols { get; }
    bool TryGetToken(string symbol, [MaybeNullWhen(false)] out IToken token);
}