using I2M.MathExpression.Interfaces;
using System;

namespace I2M.MathExpression.Operations
{
    public class OperationFactory : IOperationFactory
    {
        public Func<double, double, double> CreateLowPriorityOperation(char symbol)
        {
            return symbol switch
            {
                '+' => (a, b) => a + b,
                '-' => (a, b) => a - b,
                _ => null
            };
        }

        public Func<double, double, double> CreateHighPriorityOperation(char symbol)
        {
            return symbol switch
            {
                '*' => (a, b) => a * b,
                '/' => (a, b) => a / b,
                _ => null
            };
        }
    }
}
