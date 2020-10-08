using System;

namespace I2M.MathExpression.Interfaces
{
    public interface IOperationFactory
    {
        bool IsSupportedOperation(char symbol);
        Func<double, double, double> CreateLowPriorityOperation(char symbol);
        Func<double, double, double> CreateHighPriorityOperation(char symbol);
    }
}
