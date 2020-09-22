using I2M.MathExpression.Exceptions;
using I2M.MathExpression.Helpers;
using System;

namespace I2M.MathExpression.App
{
    internal static class Program
    {
        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter expression:");

                var expression = Console.ReadLine();

                if (expression != null && expression.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                try
                {
                    var result = MathExpressionHelper.Parse(expression).Eval();

                    Console.WriteLine($"Your result: {result}");
                }
                catch (ExpressionParseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
