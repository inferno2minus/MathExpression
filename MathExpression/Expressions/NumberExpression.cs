using I2M.MathExpression.Interfaces;

namespace I2M.MathExpression.Expressions
{
    public class NumberExpression : IExpression
    {
        private readonly double _number;

        public NumberExpression(double number)
        {
            _number = number;
        }

        public double Eval() => _number;

        public override string ToString() => $"Number: {_number}";
    }
}
