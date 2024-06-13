using Calculator.Model.Enums;

namespace Calculator.Model;

public class Token
{
    public TokenType TokenType { get; set; }
    public string Literal { get; set; }
}