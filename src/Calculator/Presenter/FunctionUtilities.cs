using System.Text.RegularExpressions;
using Calculator.Model;

namespace Calculator.Presenter;

public static class FunctionUtilities
{
    private static string _functionDeclarationPattern = @"(\w+)\(([\w,]*)\)\s*=\s*(.*)";
    
    public static bool IsCorrectFunctionDeclaration(string expression)
    {
        Match match = Regex.Match(expression, _functionDeclarationPattern);

        if (match.Success)
        {
            string body = match.Groups[3].Value;
        
            string parameters = match.Groups[2].Value;
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

        Match match = Regex.Match(expression, _functionDeclarationPattern);

        string functionName = match.Groups[1].Value;
        
        string parameters = match.Groups[2].Value;
        List<string> parametersList = new(parameters.Split(','));

        string body = match.Groups[3].Value;

        return new Function(functionName, parametersList, body);
    }

    public static string ReplaceFunctionCalls(string expression, Dictionary<string, Function> functions)
    {
        expression = expression.Replace(" ", "");
        
        string pattern = @"\b(\w+)\(([^)]*)\)";
        return Regex.Replace(expression, pattern, match =>
        {
            string functionName = match.Groups[1].Value;
            string[] args = match.Groups[2].Value.Split(',');

            if (!functions.ContainsKey(functionName))
                throw new ArgumentException($"Function '{functionName}' is not defined");

            Function function = functions[functionName];

            if (args.Length != function.Parameters.Count)
                throw new ArgumentException($"Argument count of '{functionName}' doesn't match");

            string replacedBody = function.Body;
            for (int i = 0; i < args.Length; i++)
            {
                replacedBody = replacedBody.Replace(function.Parameters[i], args[i]);
            }

            return replacedBody;
        });
    }
}