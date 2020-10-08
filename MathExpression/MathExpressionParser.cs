using I2M.MathExpression.Exceptions;
using I2M.MathExpression.Expressions;
using I2M.MathExpression.Extensions;
using I2M.MathExpression.Interfaces;
using System;
using System.Linq;

namespace I2M.MathExpression
{
    internal class MathExpressionParser : IMathExpressionParser
    {
        private readonly Func<char, Func<double, double, double>> _createOperation;
        private readonly Func<ITokenizer, IExpression> _expression;

        private static IMathExpressionParser _expressionParser;
        private static IOperationFactory _operationFactory;

        private MathExpressionParser(Func<char, Func<double, double, double>> createOperation, Func<ITokenizer, IExpression> expression)
        {
            _createOperation = createOperation;
            _expression = expression;
        }

        public static IMathExpressionParser CreateParser(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));

            var highPriorityParser = new MathExpressionParser(operationFactory.CreateHighPriorityOperation, ParseUnary);
            var lowPriorityParser = new MathExpressionParser(operationFactory.CreateLowPriorityOperation, highPriorityParser.Parse);

            _expressionParser = lowPriorityParser;

            return lowPriorityParser;
        }

        public IExpression Parse(ITokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException(nameof(tokenizer));

            var leftExpression = _expression(tokenizer);

            while (true)
            {
                tokenizer.CurrentToken.EnsureExpectedSymbol(_operationFactory);

                var operation = _createOperation(tokenizer.CurrentToken.Symbols.First());

                if (operation == null) return leftExpression;

                tokenizer.NextToken();

                var rightExpression = _expression(tokenizer);

                leftExpression = new BinaryExpression(leftExpression, rightExpression, operation);
            }
        }

        private static IExpression ParseUnary(ITokenizer tokenizer)
        {
            while (true)
            {
                if (IsPositiveTokenType(tokenizer)) continue;
                if (TryGetNegativeExpression(tokenizer, out var negativeExpression)) return negativeExpression;
                if (TyeGetNumberExpression(tokenizer, out var numberExpression)) return numberExpression;
                if (TryGetBracketExpression(tokenizer, out var bracketExpression)) return bracketExpression;

                throw new ExpressionParseException($"Unexpected symbol: {tokenizer.CurrentToken}");
            }
        }

        private static bool IsPositiveTokenType(ITokenizer tokenizer)
        {
            if (tokenizer.CurrentToken.IsAddSymbol())
            {
                tokenizer.NextToken();

                return true;
            }

            return false;
        }

        private static bool TryGetNegativeExpression(ITokenizer tokenizer, out IExpression negativeExpression)
        {
            negativeExpression = null;

            if (tokenizer.CurrentToken.IsSubtractSymbol())
            {
                tokenizer.NextToken();

                var rightExpression = ParseUnary(tokenizer);

                negativeExpression = new UnaryExpression(rightExpression, a => -a);
            }

            return negativeExpression != null;
        }

        private static bool TyeGetNumberExpression(ITokenizer tokenizer, out IExpression numberExpression)
        {
            numberExpression = null;

            if (tokenizer.CurrentToken.Number.HasValue)
            {
                numberExpression = new NumberExpression(tokenizer.CurrentToken.Number.Value);

                tokenizer.NextToken();
            }

            return numberExpression != null;
        }

        private static bool TryGetBracketExpression(ITokenizer tokenizer, out IExpression bracketExpression)
        {
            bracketExpression = null;

            if (tokenizer.CurrentToken.IsLeftBracketSymbol())
            {
                tokenizer.NextToken();

                bracketExpression = _expressionParser.Parse(tokenizer);

                tokenizer.CurrentToken.EnsureRightBracketSymbol();

                tokenizer.NextToken();
            }

            return bracketExpression != null;
        }
    }
}
