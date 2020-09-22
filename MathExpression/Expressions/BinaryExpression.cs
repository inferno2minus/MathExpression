using I2M.MathExpression.Interfaces;
using System;

namespace I2M.MathExpression.Expressions
{
    public class BinaryExpression : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;
        private readonly Func<double, double, double> _operation;

        public BinaryExpression(IExpression left, IExpression right, Func<double, double, double> operation)
        {
            _left = left;
            _right = right;
            _operation = operation;
        }

        public double Eval() => _operation(_left.Eval(), _right.Eval());
    }
}
