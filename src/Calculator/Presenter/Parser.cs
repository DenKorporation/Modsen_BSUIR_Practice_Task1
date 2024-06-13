using Calculator.Model;
using Calculator.Model.Enums;

namespace Calculator.Presenter;

public static class Parser
{
    private static Dictionary<TokenType, int> _precedence = new()
    {
        { TokenType.PLUS, 1 },
        { TokenType.MINUS, 1 },
        { TokenType.MULTIPLY, 2 },
        { TokenType.DIVIDE, 2 },
        { TokenType.LPAREN, 0 },
        { TokenType.RPAREN, 0 }
    };
    
    public static IEnumerable<Token> Parse(IEnumerable<Token> tokens)
    {
        Stack<Token> operatorStack = new();
        List<Token> output = new();
        
        foreach (var token in tokens)
        {
            if (token.TokenType == TokenType.NUMBER)
            {
                output.Add(token);
            }
            else if (token.TokenType == TokenType.LPAREN)
            {
                operatorStack.Push(token);
            }
            else if (token.TokenType == TokenType.RPAREN)
            {
                while (operatorStack.Count > 0 && operatorStack.Peek().TokenType != TokenType.LPAREN)
                {
                    output.Add(operatorStack.Pop());
                }

                operatorStack.Pop();
            }
            else if (_precedence.ContainsKey(token.TokenType))
            {
                while (operatorStack.Count > 0 &&
                       _precedence[operatorStack.Peek().TokenType] >= _precedence[token.TokenType])
                {
                    output.Add(operatorStack.Pop());
                }
                
                operatorStack.Push(token);
            }
        }

        while (operatorStack.Count > 0)
        {
            output.Add(operatorStack.Pop());
        }

        return output;
    }
}