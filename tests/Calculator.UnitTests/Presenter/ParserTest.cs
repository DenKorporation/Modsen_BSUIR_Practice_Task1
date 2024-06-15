using Calculator.Model;
using Calculator.Model.Enums;
using Calculator.Presenter;
using FluentAssertions;
using JetBrains.Annotations;

namespace Calculator.UnitTests.Presenter;

[TestSubject(typeof(Parser))]
public class ParserTest
{
    [Fact]
    public void Parse_EmptyExpression_ReturnsEmptyExpression()
    {
        IEnumerable<Token> expression = [];

        var result = Parser.Parse(expression);

        result.Should().BeEmpty();
    }

    [Fact]
    public void Parse_OneOperand_ReturnsOperand()
    {
        double value = 1.0;
        double precision = 0.01;
        IEnumerable<Token> expression = [new Token(TokenType.NUMBER, value.ToString(), value)];

        var result = Parser.Parse(expression);

        result.Should().ContainSingle();
        result.First().NumericValue.Should().BeApproximately(value, precision);
    }

    [Theory]
    [InlineData(TokenType.PLUS, "+")]
    [InlineData(TokenType.MINUS, "-")]
    [InlineData(TokenType.MULTIPLY, "*")]
    [InlineData(TokenType.DIVIDE, "/")]
    public void Parse_TwoOperandWithOperator_ReturnsRpnExpression(TokenType operatorType, string literal)
    {
        const double firstValue = 1.0;
        const double secondValue = 1.0;
        Token left = new(TokenType.NUMBER, firstValue.ToString(), firstValue),
            right = new(TokenType.NUMBER, secondValue.ToString(), secondValue),
            @operator = new(operatorType, literal, 0);

        IEnumerable<Token> expression = [left, @operator, right];
        IEnumerable<Token> expected = [left, right, @operator];

        var result = Parser.Parse(expression);

        result.Should().Equal(expected);
    }

    [Fact]
    public void Parse_ComplexExpressionWithParenthesis_ReturnsRpnExpression()
    {
        const double firstValue = 1.0, secondValue = 1.0, thirdValue = 1.0;
        Token first = new(TokenType.NUMBER, firstValue.ToString(), firstValue),
            second = new(TokenType.NUMBER, secondValue.ToString(), secondValue),
            third = new(TokenType.NUMBER, thirdValue.ToString(), thirdValue),
            lparen = new(TokenType.LPAREN, "(", 0),
            rparen = new(TokenType.RPAREN, ")", 0),
            plus = new(TokenType.PLUS, "+", 0),
            minus = new(TokenType.MINUS, "-", 0);

        // first - (second + third)
        IEnumerable<Token> expression = [first, minus, lparen, second, plus, third, rparen];
        IEnumerable<Token> expected = [first, second, third, plus, minus];

        var result = Parser.Parse(expression);

        result.Should().Equal(expected);
    }
    
    [Fact]
    public void Parse_ComplexExpressionWithOperatorsOfDifferentPrecedence_ReturnsRpnExpression()
    {
        const double firstValue = 1.0, secondValue = 1.0, thirdValue = 1.0;
        Token first = new(TokenType.NUMBER, firstValue.ToString(), firstValue),
            second = new(TokenType.NUMBER, secondValue.ToString(), secondValue),
            third = new(TokenType.NUMBER, thirdValue.ToString(), thirdValue),
            plus = new(TokenType.PLUS, "+", 0),
            multiply = new(TokenType.MULTIPLY, "*", 0);

        // first * second + third
        IEnumerable<Token> expression = [first, multiply, second, plus, third];
        IEnumerable<Token> expected = [first, second, multiply, third, plus];

        var result = Parser.Parse(expression);

        result.Should().Equal(expected);
    }
}