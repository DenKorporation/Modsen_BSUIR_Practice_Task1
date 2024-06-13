using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model.Enums
{
    public enum TokenType
    {
        NUMBER,
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
}
