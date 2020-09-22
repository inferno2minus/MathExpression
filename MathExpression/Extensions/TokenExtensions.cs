using I2M.MathExpression.Exceptions;
using I2M.MathExpression.Tokenizers;

namespace I2M.MathExpression.Extensions
{
    public static class TokenExtensions
    {
        public static void EnsureEndOfFileTokenType(this Token value)
        {
            if (value?.Type != TokenType.Eof)
            {
                throw new ExpressionParseException("Unexpected characters at end of expression");
            }
        }

        public static void EnsureRightBracketTokenType(this Token value)
        {
            if (value?.Type != TokenType.RightBracket)
            {
                throw new ExpressionParseException("Missing closing bracket");
            }
        }

        public static void EnsureNotUnknownTokenType(this Token value)
        {
            if (value?.Type == TokenType.Unknown)
            {
                throw new ExpressionParseException($"Unexpected token: {value.Type}");
            }
        }
    }
}
