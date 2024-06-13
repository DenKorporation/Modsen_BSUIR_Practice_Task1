using Calculator.Model;
using Calculator.Model.Enums;

namespace Calculator.Presenter;

/// <summary>
/// A static class that provides functionality to calculate the value of an arithmetic expression.
/// </summary>
public static class ExpressionCalculator
{
    /// <summary>
    /// Calculates the result of the given arithmetic expression.
    /// </summary>
    /// <param name="expression">An enumerable collection of tokens representing the expression.</param>
    /// <returns>The result of the expression as a double.</returns>
    /// <exception cref="ArgumentException">Thrown when the input expression is empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when an unexpected token type is encountered or the expression is invalid.</exception>
    public static double Calculate(IEnumerable<Token> expression)
    {
        if (!expression.Any())
        {
            throw new ArgumentException("The input expression cannot be empty.", nameof(expression));
        }
        var valueStack = new Stack<double>();

        foreach (var token in expression)
        {
            if (token.TokenType is TokenType.EOF or TokenType.ILLEGAL or TokenType.LPAREN or TokenType.RPAREN)
            {
                throw new InvalidOperationException($"Unexpected token type: {token.TokenType}");
            }

            if (token.TokenType == TokenType.NUMBER)
            {
                valueStack.Push(token.NumericValue);
            }
            else
            {
                HandleOperator(in token, ref valueStack);
            }
        }

        if (valueStack.Count != 1)
        {
            throw new InvalidOperationException("The expression is invalid.");
        }
        
        return valueStack.Pop();
    }

    /// <summary>
    /// Processes an operator token and applies the operation to the values on the stack.
    /// </summary>
    /// <param name="token">The operator token.</param>
    /// <param name="valueStack">The stack of values to which the operator will be applied.</param>
    /// <exception cref="InvalidOperationException">Thrown when there are fewer than 2 operands on the stack.</exception>
    /// <exception cref="DivideByZeroException">Thrown when attempting to divide by zero.</exception>
    private static void HandleOperator(in Token token, ref Stack<double> valueStack)
    {
        if (valueStack.Count < 2)
        {
            throw new InvalidOperationException("2 operands are expected");
        }

        var right = valueStack.Pop();
        var left = valueStack.Pop();
                
        switch (token.TokenType)
        {
            case TokenType.PLUS:
                valueStack.Push(left + right);
                break;
            case TokenType.MINUS:
                valueStack.Push(left - right);
                break;
            case TokenType.MULTIPLY:
                valueStack.Push(left * right);
                break;
            case TokenType.DIVIDE:
                if (right == 0)
                {
                    throw new DivideByZeroException("Division by zero is not allowed.");
                }
                valueStack.Push(left / right);
                break;
            case TokenType.POWER:
                valueStack.Push(Math.Pow(left, right));
                break;
        }   
    }
}