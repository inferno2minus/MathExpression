using System;

namespace I2M.MathExpression.Exceptions
{
    public class ExpressionParseException : Exception
    {
        public ExpressionParseException()
        {
        }

        public ExpressionParseException(string message)
            : base(message)
        {
        }

        public ExpressionParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
