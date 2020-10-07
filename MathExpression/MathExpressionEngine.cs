using I2M.MathExpression.Extensions;
using I2M.MathExpression.Interfaces;
using System;

namespace I2M.MathExpression
{
    public class MathExpressionEngine : IMathExpressionEngine
    {
        private readonly IOperationFactory _operationFactory;

        public MathExpressionEngine(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
        }

        public IExpression ParseExpression(ITokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException(nameof(tokenizer));

            tokenizer.Init();

            var expressionParser = MathExpressionParser.CreateParser(_operationFactory);

            var expression = expressionParser.Parse(tokenizer);

            tokenizer.CurrentToken.EnsureEndOfFileSymbol();

            return expression;
        }
    }
}
