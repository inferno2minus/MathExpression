using I2M.MathExpression.Exceptions;
using I2M.MathExpression.Expressions;
using I2M.MathExpression.Extensions;
using I2M.MathExpression.Interfaces;
using I2M.MathExpression.Operations;
using I2M.MathExpression.Tokenizers;
using System;

namespace I2M.MathExpression
{
    public class MathExpressionEngine : IMathExpressionEngine
    {
        private readonly IOperationFactory _operationFactory = new DefaultOperationFactory();

        public MathExpressionEngine()
        {
        }

        public MathExpressionEngine(IOperationFactory operationFactory)
        {
            _operationFactory = operationFactory;
        }

        public IExpression ParseExpression(ITokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException(nameof(tokenizer));

            tokenizer.Init();

            var expression = ParseLowPriority(tokenizer);

            tokenizer.CurrentToken.EnsureEndOfFileTokenType();

            return expression;
        }

        private IExpression ParseLowPriority(ITokenizer tokenizer)
        {
            var leftExpression = ParseHighPriority(tokenizer);

            tokenizer.CurrentToken.EnsureNotUnknownTokenType();

            while (true)
            {
                var operation = _operationFactory.CreateLowPriorityOperation(tokenizer.CurrentToken.Type);

                if (operation == null) return leftExpression;

                tokenizer.NextToken();

                var rightExpression = ParseHighPriority(tokenizer);

                leftExpression = new BinaryExpression(leftExpression, rightExpression, operation);
            }
        }

        private IExpression ParseHighPriority(ITokenizer tokenizer)
        {
            var leftExpression = ParseUnary(tokenizer);

            tokenizer.CurrentToken.EnsureNotUnknownTokenType();

            while (true)
            {
                var operation = _operationFactory.CreateHighPriorityOperation(tokenizer.CurrentToken.Type);

                if (operation == null) return leftExpression;

                tokenizer.NextToken();

                var rightExpression = ParseUnary(tokenizer);

                leftExpression = new BinaryExpression(leftExpression, rightExpression, operation);
            }
        }

        private IExpression ParseUnary(ITokenizer tokenizer)
        {
            while (true)
            {
                if (IsPositiveTokenType(tokenizer)) continue;
                if (TryGetNegativeExpression(tokenizer, out var negativeExpression)) return negativeExpression;
                if (TyeGetNumberExpression(tokenizer, out var numberExpression)) return numberExpression;
                if (TryGetBracketExpression(tokenizer, out var bracketExpression)) return bracketExpression;

                throw new ExpressionParseException($"Unexpected token: {tokenizer.CurrentToken.Type}");
            }
        }

        private static bool IsPositiveTokenType(ITokenizer tokenizer)
        {
            if (tokenizer.CurrentToken.Type == TokenType.Add)
            {
                tokenizer.NextToken();

                return true;
            }

            return false;
        }

        private bool TryGetNegativeExpression(ITokenizer tokenizer, out IExpression negativeExpression)
        {
            negativeExpression = null;

            if (tokenizer.CurrentToken.Type == TokenType.Subtract)
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

            if (tokenizer.CurrentToken.Type == TokenType.Number)
            {
                numberExpression = new NumberExpression(tokenizer.CurrentToken.Value);

                tokenizer.NextToken();
            }

            return numberExpression != null;
        }

        private bool TryGetBracketExpression(ITokenizer tokenizer, out IExpression bracketExpression)
        {
            bracketExpression = null;

            if (tokenizer.CurrentToken.Type == TokenType.LeftBracket)
            {
                tokenizer.NextToken();

                bracketExpression = ParseLowPriority(tokenizer);

                tokenizer.CurrentToken.EnsureRightBracketTokenType();

                tokenizer.NextToken();
            }

            return bracketExpression != null;
        }
    }
}
