namespace I2M.MathExpression.Tokenizers
{
    public class Token
    {
        public TokenType Type { get; set; }
        public double Value { get; set; }

        public Token(TokenType type, double value = 0)
        {
            Type = type;
            Value = value;
        }
    }
}
