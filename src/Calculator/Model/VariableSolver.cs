namespace Calculator.Model;

public static class VariableSolver
{
    private static char[] delimiters = new[] { '+', '-', '*', '/', '(', ')', '^', ' ' };

    private static bool ChekVarIsSuit(string str, string variable, int startIndex)
    {
        bool res;
        int nextCharIndex = startIndex + variable.Length;

        if ((nextCharIndex == str.Length) || ((nextCharIndex < str.Length) && (delimiters.Contains(str[nextCharIndex]))))
        {
            res = true;
        }
        else
        {
            res = false;
        }
        return res;
    }

    public static string VariableReplace(string str, List<Variable> variables)
    {
        string res = str;

        foreach (Variable v in variables)
        {
            int currentPos = 0;
            int tempPos;
            bool isEnd = false;

            while (!isEnd)
            {
                tempPos = res.IndexOf(v.Name, currentPos);

                if (tempPos != -1)
                {
                    if (ChekVarIsSuit(res, v.Name, tempPos))
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
}
