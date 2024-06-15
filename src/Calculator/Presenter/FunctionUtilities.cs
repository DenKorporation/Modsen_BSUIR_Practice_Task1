using System.Text.RegularExpressions;
using Calculator.Model;

namespace Calculator.Presenter;

public static class FunctionUtilities
{
    private static string _functionDeclarationPattern = @"(\w+)\(([\w,]*)\)\s*=\s*(.*)";
    
    public static bool IsCorrectFunctionDeclaration(string expression)
    {
        var match = Regex.Match(expression, _functionDeclarationPattern);

        if (match.Success)
        {
            var body = match.Groups[3].Value;
        
            var parameters = match.Groups[2].Value;
            List<string> parametersList = new(parameters.Split(','));

            foreach (var parameter in parametersList)
            {
                body = body.Replace(parameter, "");
            }

            foreach (var c in body)
            {
                if (char.IsAsciiLetter(c))
                    return false;
            }

            return true;
        }
        
        return false;
    }

    public static Function ParseFunction(string expression)
    {
        expression = expression.Replace(" ", "");
        
        if (!IsCorrectFunctionDeclaration(expression))
            throw new ArgumentException("Incorrect function declaration");

        var match = Regex.Match(expression, _functionDeclarationPattern);

        var functionName = match.Groups[1].Value;
        
        var parameters = match.Groups[2].Value;
        List<string> parametersList = new(parameters.Split(','));

        var body = match.Groups[3].Value;

        return new Function(functionName, parametersList, body);
    }

    public static string ReplaceFunctionCalls(string expression, Dictionary<string, Function> functions)
    {
        expression = expression.Replace(" ", "");
        
        var pattern = @"\b(\w+)\(([^)]*)\)";
        return Regex.Replace(expression, pattern, match =>
        {
            var functionName = match.Groups[1].Value;
            var args = match.Groups[2].Value.Split(',');

            if (!functions.ContainsKey(functionName))
                throw new ArgumentException($"Function '{functionName}' is not defined");

            var function = functions[functionName];

            if (args.Length != function.Parameters.Count)
                throw new ArgumentException($"Argument count of '{functionName}' doesn't match");

            var replacedBody = function.Body;
            var substitutions = function.Parameters
                .Zip(args, (from, to) => new { From = from, To = to });
            foreach (var s in substitutions)
            {
                replacedBody = replacedBody.Replace(s.From, s.To);
            }

            return $"({replacedBody})";
        });
    }
}