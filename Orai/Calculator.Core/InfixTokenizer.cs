using Calculator.Core.Tokens;
using Calculator.Core.Tokens.Operators;

namespace Calculator.Core;

internal sealed class InfixTokenizer : ITokenizer
{
    private sealed class Parenthesis : Operator
    {
        public Parenthesis(bool isLeft) => IsLeft = isLeft;

        public bool IsLeft { get; }

        public override int Precedence { get; }

        public override bool IsRightAssociative { get; }

        public override void Apply(INumberStack stack)
            => throw new NotImplementedException();
    }

    private readonly Dictionary<string, IToken> _table;
    private readonly HashSet<char> _operatorChars;

    public InfixTokenizer()
    {
        _table = new Dictionary<string, IToken>()
        {
            ["+"] = new AddOperator(),
            ["-"] = new SubtractOperator(),
            ["*"] = new MultiplyOperator(),
            ["/"] = new DivideOperator(),
            ["^"] = new PowerOperator(),
            ["sin"] = new SinusFunction(),
        };
        _operatorChars = _table.Keys.SelectMany(c => c).ToHashSet();
    }

    public IEnumerable<IToken> Tokenize(string expression)
    {
        var output = new List<IToken>();
        var operatorStack = new Stack<Operator>();
        var tokens = CreateTokens(expression);

        foreach (var token in tokens)
        {
            switch (token)
            {
                case NumberToken n:
                    output.Add(n);
                    break;
                case Parenthesis p:
                    if (p.IsLeft)
                    {
                        operatorStack.Push(p);
                    }
                    else
                    {
                        while (operatorStack.Count > 0 && operatorStack.Peek() is not Parenthesis)
                        {
                            output.Add(operatorStack.Pop());
                        }
                        if (operatorStack.Count == 0 || operatorStack.Pop() is not Parenthesis)
                            throw new ArgumentException("Mismatched parentheses");
                    }
                    break;
                case Operator o:
                    while (operatorStack.Count > 0)
                    {
                        var top = operatorStack.Peek();
                        if ((o.IsRightAssociative && o.Precedence < top.Precedence) ||
                            (!o.IsRightAssociative && o.Precedence <= top.Precedence))
                        {
                            output.Add(operatorStack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    operatorStack.Push(o);
                    break;
            }
        }
        while (operatorStack.Count > 0)
        {
            var op = operatorStack.Pop();

            if (op is Parenthesis)
                throw new ArgumentException("Mismatched parentheses");

            output.Add(op);
        }
        return output;
    }

    private IEnumerable<IToken> CreateTokens(string input)
    {
        int i = 0;
        while (i < input.Length)
        {
            if (char.IsWhiteSpace(input[i]))
            {
                i++;
                continue;
            }
            if (char.IsDigit(input[i]) || input[i] == '.')
            {
                int start = i;
                while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.')) i++;
                yield return new NumberToken(double.Parse(input.Substring(start, i - start)));
                continue;
            }
            if (_operatorChars.Contains(input[i]))
            {
                int start = i;
                while (i < input.Length && _operatorChars.Contains(input[i])) i++;
                string op = input.Substring(start, i - start);
                if (_table.TryGetValue(op, out IToken? token))
                {
                    yield return token;
                    continue;
                }
                else
                {
                    throw new ArgumentException($"Unknown operator: {op}");
                }
            }
            if (input[i] == '(')
            {
                yield return new Parenthesis(isLeft: true);
                i++;
                continue;
            }
            if (input[i] == ')')
            {
                yield return new Parenthesis(isLeft: false);
                i++;
                continue;
            }
            throw new ArgumentException($"Invalid character: {input[i]}");
        }
    }
}

