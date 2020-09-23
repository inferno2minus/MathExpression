using I2M.MathExpression.Interfaces;
using System;

namespace I2M.MathExpression.Expressions
{
    public class UnaryExpression : IExpression
    {
        private readonly IExpression _right;
        private readonly Func<double, double> _operation;

        public UnaryExpression(IExpression right, Func<double, double> operation)
        {
            _right = right;
            _operation = operation;
        }

        public double Eval() => _operation(_right.Eval());

        public override string ToString() => $"Right: {_right.Eval()}, Result: {_operation(_right.Eval())}";
    }
}
