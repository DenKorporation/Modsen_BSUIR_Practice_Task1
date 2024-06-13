using Calculator.Model.Enums;

namespace Calculator.Model;

public class Token
{
    public TokenType TokenType { get; set; }
    public string Literal { get; set; }
    public double NumericValue { get; set; }

    public Token()
    {
        TokenType = TokenType.EOF;
        Literal = "";
        NumericValue = 0;
    }

    public Token(TokenType Type, string StrValue, double Value)
    {
        TokenType = Type; 
        Literal = StrValue;
        NumericValue = Value;
    }

}
