using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model
{
    public enum TokenType 
    {
        NUBMER,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        POWER,
        LPAREN,
        RPAREN,
        EOF,
        ILLEGAL
    }

    internal class Token
    {
        TokenType type;
        string strValue;
        double value;

        public TokenType Type
        {
            get { return type; }
        }

        public string StringValue
        {
            get { return strValue; }
        }

        public double NumericValue
        {
            get { return value; }
        }

        public Token()
        {
            type = TokenType.EOF;
        }

        public Token(TokenType Type, string StrValue, double Value)
        {
            type = Type; 
            strValue = StrValue;
            value = Value;
        }

    }
}
