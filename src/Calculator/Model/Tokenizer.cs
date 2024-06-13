using Calculator.Model.Enums;
using System.Globalization;
namespace Calculator.Model;

public static class Tokenizer
{
    private static void FinishNumber(ref List<Token> tokens, ref string tempNumber, ref bool isPointUsed, ref bool isSucceed)
    {
        if ((tempNumber.Length > 0) && (tempNumber[tempNumber.Length - 1] == '.'))
        {
            isSucceed = false;
        }

        if (tempNumber.Length > 0)
        {
            tokens.Add(new Token(TokenType.NUMBER, tempNumber, float.Parse(tempNumber, CultureInfo.InvariantCulture)));
            tempNumber = "";
            isPointUsed = false;
        }
    }

    private static void HandleOperator(ref List<Token> tokens, char s, ref bool isSucceed)
    {
        switch (s)
        {
            case '+':
                tokens.Add(new Token(TokenType.PLUS, "+", 0));
                break;
            case '-':
                tokens.Add(new Token(TokenType.MINUS, "-", 0));
                break;
            case '*':
                tokens.Add(new Token(TokenType.MULTIPLY, "*", 0));
                break;
            case '/':
                tokens.Add(new Token(TokenType.DIVIDE, "/", 0));
                break;
            case '^':
                tokens.Add(new Token(TokenType.POWER, "^", 0));
                break;
            case '(':
                tokens.Add(new Token(TokenType.LPAREN, "(", 0));
                break;
            case ')':
                tokens.Add(new Token(TokenType.RPAREN, ")", 0));
                break;
            default:
                isSucceed = false;
                break;
        }
    }

    private static void HandleNumber(ref List<Token> tokens, char s, ref string tempNumber, ref bool isPointUsed, ref bool isSucceed)
    {
        if (s == '.')
        {
            if ((!isPointUsed) && (tempNumber.Length > 0))
            {
                tempNumber += s;
                isPointUsed = true;
            }
            else
            {
                isSucceed = false;
            }
        }
        else
        {
            tempNumber += s;
        }
    }

    public static List<Token> ConvertStringToTokens(string str)
    {
        List<Token> res = new List<Token>();
        bool isSucceed = true;
        string tempNumber = "";
        bool isPointUsed = false;

        str = str.Replace(" ", "");

        foreach (char s in str)
        {
            if ( (char.IsDigit(s)) || (s == '.'))
            {
                HandleNumber(ref res,s,ref tempNumber, ref isPointUsed, ref isSucceed);
            }
            else
            {
                FinishNumber(ref res,ref tempNumber, ref isPointUsed, ref isSucceed);
                HandleOperator(ref res, s, ref isSucceed);
            }
        }

        FinishNumber(ref res, ref tempNumber, ref isPointUsed, ref isSucceed);

        if (!isSucceed)
        {
            res.Clear();
            res.Add(new Token(TokenType.ILLEGAL, "", 0));
        }

        return res;
    }

}
