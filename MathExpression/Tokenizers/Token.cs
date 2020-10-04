namespace I2M.MathExpression.Tokenizers
{
    public class Token
    {
        public char Symbol { get; }
        public double? Number { get; }

        public Token(char symbol, double? number = null)
        {
            Symbol = symbol;
            Number = number;
        }
    }
}
