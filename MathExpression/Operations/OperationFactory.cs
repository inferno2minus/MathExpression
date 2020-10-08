using I2M.MathExpression.Interfaces;
using System;
using System.Linq;

namespace I2M.MathExpression.Operations
{
    public class OperationFactory : IOperationFactory
    {
        private readonly char[] _supportedOperations = { '+', '-', '*', '/' };

        public bool IsSupportedOperation(char symbol)
        {
            return _supportedOperations.Contains(symbol);
        }

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
