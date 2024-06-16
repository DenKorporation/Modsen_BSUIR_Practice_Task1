using System.Collections;
using Calculator.Model;
using Calculator.Model.Enums;
using Calculator.Presenter;
using FluentAssertions;
using JetBrains.Annotations;

namespace Calculator.UnitTests.Presenter;

[TestSubject(typeof(FunctionUtilities))]
public class FunctionUtilitiesTest
{
    private class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            ["f(x,y)=x+3*5-y", "f", new List<string>{"x", "y"}, "x+3*5-y"],
            ["g(param1,param2)=param1*param2", "g", new List<string>{"param1", "param2"}, "param1*param2"]
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    [Theory]
    [InlineData("f(x,y)=x+3*5-y")]
    [InlineData("g(param1,param2)=param1*param2")]
    public void IsCorrectFunctionDeclaration_CorrectFunctionDeclaration_ReturnsTrue(string functionDeclaration)
    {
        var result = FunctionUtilities.IsCorrectFunctionDeclaration(functionDeclaration);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("fx,y)=x+3*5-y")]
    [InlineData("f(=3")]
    [InlineData("g()")]
    public void IsCorrectFunctionDeclaration_IncorrectFunctionDeclaration_ReturnsFalse(string functionDeclaration)
    {
        var result = FunctionUtilities.IsCorrectFunctionDeclaration(functionDeclaration);

        result.Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void ParseFunction_CorrectFunctionDeclaration_ReturnsFunctionObject(string expression, string name,
        List<string> parameters, string body)
    {
        var expected = new Function(name, parameters, body);

        var result = FunctionUtilities.ParseFunction(expression);

        result.Should().BeEquivalentTo(expected);
    }
    
    [Theory]
    [InlineData("fx,y)=x+3*5-y")]
    [InlineData("f(=3")]
    [InlineData("g()")]
    public void ParseFunction_IncorrectFunctionDeclaration_ThrowsArgumentException(string functionDeclaration)
    {
        Action action = () => FunctionUtilities.ParseFunction(functionDeclaration);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ReplaceFunctionCalls_FunctionIsNotDefined_ThrowsArgumentException()
    {
        Dictionary<string, Function> functions = [];
        var expression = "f(3,5)";

        Action action = () => FunctionUtilities.ReplaceFunctionCalls(expression, functions);

        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void ReplaceFunctionCalls_ArgumentCountMismatch_ThrowsArgumentException()
    {
        Dictionary<string, Function> functions = new(){ ["f"] = new Function("f", ["x"], "x+3*5")};
        var expression = "f(3,5)";

        Action action = () => FunctionUtilities.ReplaceFunctionCalls(expression, functions);

        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void ReplaceFunctionCalls_ValidExpression_ReturnsExpressionWithReplacedFunction()
    {
        Dictionary<string, Function> functions = new(){ ["f"] = new Function("f", ["x", "y"], "x+3*5-y")};
        const string expression = "f(3,5)";
        const string expected = "(3+3*5-5)";

        var result = FunctionUtilities.ReplaceFunctionCalls(expression, functions);

        result.Should().Be(expected);
    }
}