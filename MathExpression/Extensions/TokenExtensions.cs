using I2M.MathExpression.Exceptions;
using I2M.MathExpression.infrastructure;
using I2M.MathExpression.Tokenizers;

namespace I2M.MathExpression.Extensions
{
    public static class TokenExtensions
    {
        public static void EnsureEndOfFileSymbol(this Token value)
        {
            if (value?.Symbol != Symbols.Eof)
            {
                throw new ExpressionParseException("Unexpected symbol in expression");
            }
        }

        public static void EnsureRightBracketSymbol(this Token value)
        {
            if (value?.Symbol != Symbols.RightBracket)
            {
                throw new ExpressionParseException("Missing closing bracket");
            }
        }

        public static bool IsAddSymbol(this Token value)
        {
            return value?.Symbol == Symbols.Add;
        }

        public static bool IsSubtractSymbol(this Token value)
        {
            return value?.Symbol == Symbols.Subtract;
        }

        public static bool IsLeftBracketSymbol(this Token value)
        {
            return value?.Symbol == Symbols.LeftBracket;
        }
    }
}
