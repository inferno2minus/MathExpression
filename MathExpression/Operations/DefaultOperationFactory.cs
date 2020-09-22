using I2M.MathExpression.Interfaces;
using I2M.MathExpression.Tokenizers;
using System;

namespace I2M.MathExpression.Operations
{
    public class DefaultOperationFactory : IOperationFactory
    {
        public Func<double, double, double> CreateOperation(TokenType type)
        {
            return type switch
            {
                TokenType.Add => (a, b) => a + b,
                TokenType.Subtract => (a, b) => a - b,
                TokenType.Multiply => (a, b) => a * b,
                TokenType.Divide => (a, b) => a / b,
                _ => null
            };
        }
    }
}
