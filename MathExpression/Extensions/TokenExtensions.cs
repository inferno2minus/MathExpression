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
                throw new MathExpressionParseException("Unexpected characters at end of expression");
            }
        }

        public static void EnsureRightBracketTokenType(this Token value)
        {
            if (value?.Type != TokenType.RightBracket)
            {
                throw new MathExpressionParseException("Missing closing bracket");
            }
        }

        public static void EnsureNotUnknownTokenType(this Token value)
        {
            if (value?.Type == TokenType.Unknown)
            {
                throw new MathExpressionParseException($"Unexpected token: {value.Type}");
            }
        }
    }
}
