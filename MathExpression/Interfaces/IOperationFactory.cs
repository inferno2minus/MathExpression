using I2M.MathExpression.Tokenizers;
using System;

namespace I2M.MathExpression.Interfaces
{
    public interface IOperationFactory
    {
        Func<double, double, double> CreateLowPriorityOperation(TokenType type);
        Func<double, double, double> CreateHighPriorityOperation(TokenType type);
    }
}
