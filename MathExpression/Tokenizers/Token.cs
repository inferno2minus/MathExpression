namespace I2M.MathExpression.Tokenizers
{
    public class Token
    {
        public TokenType Type { get; }
        public double Value { get; }

        public Token(TokenType type, double value = 0)
        {
            Type = type;
            Value = value;
        }
    }
}
