namespace Calculator.Model;

public class Function
{
    public string Name { get; set; }
    public List<string> Parameters { get; set; }
    public string Body { get; set; }

    public Function(string name, List<string> parameters, string body)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
    }

    public override string ToString()
    {
        return $"{Name}({string.Join(',', Parameters)})={Body}";
    }
}