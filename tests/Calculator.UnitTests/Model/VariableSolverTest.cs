using System.Globalization;
using Calculator.Model;
using FluentAssertions;
using JetBrains.Annotations;

namespace Calculator.UnitTests.Model;

[TestSubject(typeof(VariableSolver))]
public class VariableSolverTest
{
    public VariableSolverTest()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
    }
    
    [Fact]
    public void ParseVariable_ValidExpression_ReturnsVariableObject()
    {
        string expression = "x=0.6";
        Variable expected = new Variable("x", 0.6);

        var result = VariableSolver.ParseVariable(expression);

        result.Should().BeEquivalentTo(expected);
    }
    
    [Theory]
    [InlineData("x*=1")]
    [InlineData("x=")]
    [InlineData("x 1")]
    [InlineData("=1")]
    public void ParseVariable_InvalidExpression_ThrowsArgumentException(string expression)
    {
        Action action = () => VariableSolver.ParseVariable(expression);

        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("x+3", "3+3", 3)]
    [InlineData("1.5+4*(x-7)", "1.5+4*(4.5-7)", 4.5)]
    public void VariableReplace_ValidExpression_ReturnsExpressionWithoutVariable(string expression, string expected, double value)
    {
        List<Variable> variables = [new Variable("x", value)];

        var result = VariableSolver.VariableReplace(expression, variables);

        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("x+3", "x+3")]
    [InlineData("1.5+4*(x-7)", "1.5+4*(x-7)")]
    public void VariableReplace_ExpressionWithUndefinedVariable_ReturnsExpressionUnchanged(string expression, string expected)
    {
        List<Variable> variables = [];

        var result = VariableSolver.VariableReplace(expression, variables);

        result.Should().Be(expected);
    }
}