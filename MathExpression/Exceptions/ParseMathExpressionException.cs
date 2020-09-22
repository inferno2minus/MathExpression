using System;

namespace I2M.MathExpression.Exceptions
{
    public class ParseMathExpressionException : Exception
    {
        public ParseMathExpressionException()
        {
        }

        public ParseMathExpressionException(string message)
            : base(message)
        {
        }

        public ParseMathExpressionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
