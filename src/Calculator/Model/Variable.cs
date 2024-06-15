namespace Calculator.Model
{
    public class Variable
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public Variable(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}
