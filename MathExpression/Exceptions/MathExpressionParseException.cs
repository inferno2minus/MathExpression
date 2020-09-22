using System;

namespace I2M.MathExpression.Exceptions
{
    public class MathExpressionParseException : Exception
    {
        public MathExpressionParseException()
        {
        }

        public MathExpressionParseException(string message)
            : base(message)
        {
        }

        public MathExpressionParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
