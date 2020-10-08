using I2M.MathExpression.Exceptions;
using I2M.MathExpression.Infrastructure;
using I2M.MathExpression.Interfaces;
using I2M.MathExpression.Tokenizers;
using System.Linq;

namespace I2M.MathExpression.Extensions
{
    public static class TokenExtensions
    {
        public static void EnsureExpectedSymbol(this Token value, IOperationFactory operationFactory)
        {
            var supportedSymbols = new[] { Symbols.Eof, Symbols.RightBracket };

            if (value == null || operationFactory == null) return;

            var symbol = value.Symbols.First();

            if (!supportedSymbols.Contains(symbol) && !operationFactory.IsSupportedOperation(symbol))
            {
                throw new ExpressionParseException($"Unexpected symbol: {value}");
            }
        }

        public static void EnsureEndOfFileSymbol(this Token value)
        {
            if (value?.Symbols.First() != Symbols.Eof)
            {
                throw new ExpressionParseException("Unexpected symbol in expression");
            }
        }

        public static void EnsureRightBracketSymbol(this Token value)
        {
            if (value?.Symbols.First() != Symbols.RightBracket)
            {
                throw new ExpressionParseException("Missing closing bracket");
            }
        }

        public static bool IsAddSymbol(this Token value)
        {
            return value?.Symbols.First() == Symbols.Add;
        }

        public static bool IsSubtractSymbol(this Token value)
        {
            return value?.Symbols.First() == Symbols.Subtract;
        }

        public static bool IsLeftBracketSymbol(this Token value)
        {
            return value?.Symbols.First() == Symbols.LeftBracket;
        }
    }
}
