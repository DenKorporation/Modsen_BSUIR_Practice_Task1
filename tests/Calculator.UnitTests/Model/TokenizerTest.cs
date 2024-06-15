using System.Collections;
using System.Globalization;
using Calculator.Model;
using Calculator.Model.Enums;
using FluentAssertions;
using JetBrains.Annotations;

namespace Calculator.UnitTests.Model;

[TestSubject(typeof(Tokenizer))]
public class TokenizerTest
{
    private class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            [1.0, new Token(TokenType.PLUS, "+", 0), 2.0, "1+2"],
            [1.0, new Token(TokenType.MINUS, "-", 0), 2.0, "1-2"],
            [1.0, new Token(TokenType.MULTIPLY, "*", 0), 2.0, "1*2"],
            [1.0, new Token(TokenType.DIVIDE, "/", 0), 2.0, "1/2"],
            [1.0, new Token(TokenType.POWER, "^", 0), 2.0, "1^2"],
            [1.5, new Token(TokenType.PLUS, "+", 0), 0.5, "1.5+0.5"]
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void ConvertStringToTokens_ValidExpressionWithTwoOperatorsAndOneOperand_ReturnsValidListOfToken(
        double leftValue,
        Token operatorToken, double rightValue, string expression)
    {
        List<Token> expected =
        [
            new Token(TokenType.NUMBER, leftValue.ToString(CultureInfo.InvariantCulture), leftValue),
            operatorToken,
            new Token(TokenType.NUMBER, rightValue.ToString(CultureInfo.InvariantCulture), rightValue)
        ];

        var result = Tokenizer.ConvertStringToTokens(expression);

        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("1.")]
    [InlineData("1.11.1")]
    [InlineData("1..1")]
    public void ConvertStringToTokens_InvalidNumber_ReturnsIllegalToken(string expression)
    {
        List<Token> expected = [new Token(TokenType.ILLEGAL, "", 0)];

        var result = Tokenizer.ConvertStringToTokens(expression);

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ConvertStringToTokens_InvalidExpression_ReturnsIllegalToken()
    {
        List<Token> expected = [new Token(TokenType.ILLEGAL, "", 0)];

        var result = Tokenizer.ConvertStringToTokens("1&3");

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ConvertStringToTokens_ExpressionWithParenthesis_ReturnsValidListOfToken()
    {
        List<Token> expected =
        [
            new Token(TokenType.NUMBER, "1", 1.0),
            new Token(TokenType.MULTIPLY, "*", 0.0),
            new Token(TokenType.LPAREN, "(", 0.0),
            new Token(TokenType.NUMBER, "2", 2.0),
            new Token(TokenType.PLUS, "+", 0.0),
            new Token(TokenType.NUMBER, "3", 3.0),
            new Token(TokenType.RPAREN, ")", 0.0),
        ];

        var result = Tokenizer.ConvertStringToTokens("1*(2+3)");

        result.Should().BeEquivalentTo(expected);
    }
}