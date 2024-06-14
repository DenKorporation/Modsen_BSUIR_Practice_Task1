using Calculator.Model;
using Calculator.Model.Enums;
using Calculator.Presenter;
using JetBrains.Annotations;
using FluentAssertions;

namespace Calculator.UnitTests.Presenter;

[TestSubject(typeof(ExpressionCalculator))]
public class ExpressionCalculatorTest
{
    [Fact]
    public void Calculate_EmptyExpression_ThrowsArgumentException()
    {
        IEnumerable<Token> emptyExpression = [];

        Action calculation = () => ExpressionCalculator.Calculate(emptyExpression);

        calculation.Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(TokenType.EOF, "")]
    [InlineData(TokenType.ILLEGAL, "")]
    [InlineData(TokenType.LPAREN, "(")]
    [InlineData(TokenType.RPAREN, ")")]
    public void Calculate_UnexpectedTokenType_ThrowsInvalidOperationException(TokenType type, string literal)
    {
        IEnumerable<Token> emptyExpression = 
        [
            new Token(type, literal, 0)
        ];

        Action calculation = () => ExpressionCalculator.Calculate(emptyExpression);

        calculation.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void Calculate_OneOperandWithoutOperator_ReturnsOperand()
    {
        double operandValue = 1.0;
        double precision = 0.001;
        IEnumerable<Token> expression = [new Token(TokenType.NUMBER, operandValue.ToString(), operandValue)];

        double result = ExpressionCalculator.Calculate(expression);

        result.Should().BeApproximately(operandValue, precision);
    }
    
    [Fact]
    public void Calculate_LessThanTwoOperandsWithOneOperator_ThrowsInvalidOperationException()
    {
        double value = 3.0;
        IEnumerable<Token> expression = 
        [
            new Token(TokenType.NUMBER, value.ToString(), value), 
            new Token(TokenType.PLUS, "+", 0)
        ];

        Action calculation = () => ExpressionCalculator.Calculate(expression);

        calculation.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void Calculate_TwoOperandsWithoutOperator_ThrowsInvalidOperationException()
    {
        double operandValue = 1.0;
        var token = new Token(TokenType.NUMBER, operandValue.ToString(), operandValue);
        IEnumerable<Token> expression = [token, token];

        Action calculation = () => ExpressionCalculator.Calculate(expression);

        calculation.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void Calculate_TwoOperandsWithPlusOperator_ReturnsSumOfTwoOperands()
    {
        double firstValue = 1.0, secondValue = 2.5;
        double expected = 3.5;
        double precision = 0.001;
        IEnumerable<Token> expression = 
        [
            new Token(TokenType.NUMBER, firstValue.ToString(), firstValue), 
            new Token(TokenType.NUMBER, secondValue.ToString(), secondValue),
            new Token(TokenType.PLUS, "+", 0)
        ];

        var result = ExpressionCalculator.Calculate(expression);

        result.Should().BeApproximately(expected, precision);
    }
    
    [Fact]
    public void Calculate_TwoOperandsWithMinusOperator_ReturnsDifferenceOfTwoOperands()
    {
        double firstValue = 1.0, secondValue = 2.5;
        double expected = -1.5;
        double precision = 0.001;
        IEnumerable<Token> expression = 
        [
            new Token(TokenType.NUMBER, firstValue.ToString(), firstValue), 
            new Token(TokenType.NUMBER, secondValue.ToString(), secondValue),
            new Token(TokenType.MINUS, "-", 0)
        ];

        var result = ExpressionCalculator.Calculate(expression);

        result.Should().BeApproximately(expected, precision);
    }
    
    [Fact]
    public void Calculate_TwoOperandsWithMultiplyOperator_ReturnsProductOfTwoOperands()
    {
        double firstValue = 1.0, secondValue = 2.5;
        double expected = 2.5;
        double precision = 0.001;
        IEnumerable<Token> expression = 
        [
            new Token(TokenType.NUMBER, firstValue.ToString(), firstValue), 
            new Token(TokenType.NUMBER, secondValue.ToString(), secondValue),
            new Token(TokenType.MULTIPLY, "*", 0)
        ];

        var result = ExpressionCalculator.Calculate(expression);

        result.Should().BeApproximately(expected, precision);
    }
    
    [Fact]
    public void Calculate_TwoOperandsWithDivideOperator_ReturnsRatioOfTwoOperands()
    {
        double firstValue = 3.0, secondValue = 2.0;
        double expected = 1.5;
        double precision = 0.001;
        IEnumerable<Token> expression = 
        [
            new Token(TokenType.NUMBER, firstValue.ToString(), firstValue), 
            new Token(TokenType.NUMBER, secondValue.ToString(), secondValue),
            new Token(TokenType.DIVIDE, "/", 0)
        ];

        var result = ExpressionCalculator.Calculate(expression);

        result.Should().BeApproximately(expected, precision);
    }
    
    [Fact]
    public void Calculate_DivisionByZero_ThrowsDivideByZeroException()
    {
        double firstValue = 3.0, secondValue = 0.0;
        IEnumerable<Token> expression = 
        [
            new Token(TokenType.NUMBER, firstValue.ToString(), firstValue), 
            new Token(TokenType.NUMBER, secondValue.ToString(), secondValue),
            new Token(TokenType.DIVIDE, "/", 0)
        ];

        Action calculation = () => ExpressionCalculator.Calculate(expression);

        calculation.Should().Throw<DivideByZeroException>();
    }
    
    [Fact]
    public void Calculate_TwoOperandsWithPowerOperator_ReturnsPowerOfTwoOperands()
    {
        double firstValue = 3.0, secondValue = 2.0;
        double expected = 9.0;
        double precision = 0.001;
        IEnumerable<Token> expression = 
        [
            new Token(TokenType.NUMBER, firstValue.ToString(), firstValue), 
            new Token(TokenType.NUMBER, secondValue.ToString(), secondValue),
            new Token(TokenType.POWER, "^", 0)
        ];

        var result = ExpressionCalculator.Calculate(expression);

        result.Should().BeApproximately(expected, precision);
    }
}