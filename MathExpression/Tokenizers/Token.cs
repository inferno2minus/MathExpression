using System.Collections.Generic;

namespace I2M.MathExpression.Tokenizers
{
    public class Token
    {
        public IEnumerable<char> Symbols { get; }
        public double? Number { get; }

        public Token(IEnumerable<char> symbols, double? number = null)
        {
            Symbols = symbols;
            Number = number;
        }
    }
}
