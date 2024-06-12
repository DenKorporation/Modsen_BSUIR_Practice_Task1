using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model
{
    internal static class Tokenizer
    {

        static private string RemoveSpaces(string str)
        {
            string res = "";

            foreach (char s in str)
            {
                if (s != ' ')
                {
                    res += s;
                }
            }

            return res;
        }

        public static List<Token> ConvertStringToTokens(string str)
        {
            List<Token> res = new List<Token>();
            bool isSucceed = true;
            RemoveSpaces(str);

            string tempNumber = "";
            bool isPointUsed = false;

            foreach (char s in str)
            {
                if ( ((s >= '0') && (s <= '9')) || (s == '.'))
                {
                    if (s == '.')
                    {
                        if ((!isPointUsed) && (tempNumber.Length>0))
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
                else
                {
                    if ((tempNumber.Length > 0) && (tempNumber[tempNumber.Length - 1] == '.'))
                    {
                        isSucceed = false;
                    }

                    if (tempNumber.Length > 0)
                    {
                        res.Add(new Token(TokenType.NUBMER, tempNumber, float.Parse(tempNumber, CultureInfo.InvariantCulture)));
                        tempNumber = "";
                        isPointUsed = false;
                    }

                    switch (s) 
                    {
                        case '+':
                            res.Add(new Token(TokenType.PLUS, "+", 0));
                            break;
                        case '-':
                            res.Add(new Token(TokenType.MINUS, "-", 0));
                            break;
                        case '*':
                            res.Add(new Token(TokenType.MULTIPLY, "*", 0));
                            break;
                        case '/':
                            res.Add(new Token(TokenType.DIVIDE, "/", 0));
                            break;
                        case '^':
                            res.Add(new Token(TokenType.POWER, "^", 0));
                            break;
                        case '(':
                            res.Add(new Token(TokenType.LPAREN, "(", 0));
                            break;
                        case ')':
                            res.Add(new Token(TokenType.RPAREN, ")", 0));
                            break;
                        default:
                            isSucceed = false;
                            break;
                    }
                }

            }

            if ((tempNumber.Length > 0) && (tempNumber[tempNumber.Length - 1] == '.'))
            {
                isSucceed = false;
            }

            if (tempNumber.Length > 0)
            {
                res.Add(new Token(TokenType.NUBMER, tempNumber, float.Parse(tempNumber, CultureInfo.InvariantCulture)));
                tempNumber = "";
                isPointUsed = false;
            }

            if (!isSucceed)
            {
                res.Clear();
                res.Add(new Token(TokenType.ILLEGAL, "", 0));
            }

            return res;
        }

    }
}
