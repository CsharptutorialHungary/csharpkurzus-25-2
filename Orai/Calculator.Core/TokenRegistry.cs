using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Calculator.Core.Tokens;

namespace Calculator.Core;

internal class TokenRegistry : ITokenRegistry
{
    readonly Dictionary<string, IToken> _tokens;

    public TokenRegistry() => _tokens = DiscoverTokens();

    public bool TryGetToken(string symbol, [MaybeNullWhen(false)] out IToken token)
    {
        return _tokens.TryGetValue(symbol, out token);
    }

    private static Dictionary<string, IToken> DiscoverTokens()
    {
        Assembly currentAssembly = GetCurrentAssembly();
        IEnumerable<Type> tokenTypes = GetTokenTypes(currentAssembly);

        return BuildTokenDictionary(tokenTypes);
    }

    private static Assembly GetCurrentAssembly() => typeof(TokenRegistry).Assembly;

    private static IEnumerable<Type> GetTokenTypes(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(type => type.IsAssignableTo(typeof(IToken)))
            .Where(tokenType => tokenType is
            {
                IsClass: true,
                IsAbstract: false,
                IsGenericType: false
            });
    }

    private static Dictionary<string, IToken> BuildTokenDictionary(IEnumerable<Type> tokenTypes)
    {
        Dictionary<string, IToken> tokenDictionary = [];

        foreach (Type tokenType in tokenTypes)
        {
            OperatorAttribute? operatorAttribute = tokenType.GetCustomAttribute<OperatorAttribute>();

            if (operatorAttribute is null)
            {
                continue;
            }

            if (Activator.CreateInstance(tokenType) is IToken token)
            {
                tokenDictionary[operatorAttribute.Symbol] = token;
            }
        }

        return tokenDictionary;
    }
}
