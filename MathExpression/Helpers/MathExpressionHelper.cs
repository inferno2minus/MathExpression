using I2M.MathExpression.Interfaces;
using I2M.MathExpression.Operations;
using I2M.MathExpression.Tokenizers;
using System;
using System.IO;

namespace I2M.MathExpression.Helpers
{
    public static class MathExpressionHelper
    {
        public static IExpression Parse(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            using var reader = new StringReader(value);
            var tokenizer = new Tokenizer(reader);
            var operationFactory = new OperationFactory();
            var engine = new MathExpressionEngine(operationFactory);

            return engine.ParseExpression(tokenizer);
        }
    }
}
