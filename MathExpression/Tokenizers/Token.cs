namespace I2M.MathExpression.Tokenizers
{
    public class Token
    {
        public char Symbol { get; }
        public double Value { get; }

        public Token(char symbol, double value = 0)
        {
            Symbol = symbol;
            Value = value;
        }
    }
}
