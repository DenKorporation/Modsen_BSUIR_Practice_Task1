using System.Text.RegularExpressions;

namespace Calculator.Model;

public static class VariableSolver
{
    private static char[] delimiters = new[] { '+', '-', '*', '/', '(', ')', '^', ' ' };

    private static bool CheсkVariableIsSuit(string expression, string variable, int startIndex)
    {
        var res = true;
        var nextCharIndex = startIndex + variable.Length;
        var prevCharIndex = startIndex - 1;
        res = ((nextCharIndex == expression.Length) || ((nextCharIndex < expression.Length) && (delimiters.Contains(expression[nextCharIndex]))))
            && ((prevCharIndex == -1) || (delimiters.Contains(expression[prevCharIndex])));
        return res;
    }

    public static string VariableReplace(string expression, List<Variable> variables)
    {
        string res = expression.Replace(" ", "");

        foreach (Variable v in variables)
        {
            var currentPos = 0;
            var tempPos = 0;
            var isEnd = false;

            while (!isEnd)
            {
                tempPos = res.IndexOf(v.Name, currentPos);

                if (tempPos != -1)
                {
                    if (CheсkVariableIsSuit(res, v.Name, tempPos))
                    {
                        res = res.Substring(0, tempPos) + v.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)
                            + res.Substring(tempPos + v.Name.Length);
                    }

                    currentPos = tempPos + 1;
                }
                else
                {
                    isEnd = true;
                }

            }
        }

        return res;
    }

    public static Variable ParseVariable(string expression)
    {
        var pattern = @"^\s*(\w+)\s*=\s*(\d+\.?\d*)\s*$";

        var match = Regex.Match(expression, pattern);

        if (!match.Success)
            throw new ArgumentException("Incorrect variable declaration");

        var variableName = match.Groups[1].Value;
        var variableValue = double.Parse(match.Groups[2].Value);

        return new Variable(variableName, variableValue);
    }
}
