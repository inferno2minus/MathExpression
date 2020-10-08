using System.Collections.Generic;
using System.Linq;

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

        public override string ToString() => Symbols.First() == Infrastructure.Symbols.Eof ? "<EOF>" : new string(Symbols.ToArray());
    }
}
