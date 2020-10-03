using I2M.MathExpression.Exceptions;
using I2M.MathExpression.Operations;
using I2M.MathExpression.Tokenizers;
using System;
using System.IO;

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

                if (expression == null) continue;

                if (expression.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                try
                {
                    using var reader = new StringReader(expression);
                    var tokenizer = new Tokenizer(reader);
                    var operationFactory = new OperationFactory();
                    var engine = new MathExpressionEngine(operationFactory);

                    var result = engine.ParseExpression(tokenizer).Eval();

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
