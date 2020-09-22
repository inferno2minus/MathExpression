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
        private IOperationFactory OperationFactory { get; } = new DefaultOperationFactory();

        public MathExpressionEngine()
        {
        }

        public MathExpressionEngine(IOperationFactory operationFactory)
        {
            OperationFactory = operationFactory;
        }

        public IExpression ParseExpression(ITokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException(nameof(tokenizer));

            tokenizer.Init();

            var expression = ParseHighPriority(tokenizer);

            tokenizer.CurrentToken.EnsureEndOfFileTokenType();

            return expression;
        }

        private IExpression ParseHighPriority(ITokenizer tokenizer)
        {
            var left = ParseLowPriority(tokenizer);

            tokenizer.CurrentToken.EnsureNotUnknownTokenType();

            while (true)
            {
                var operation = OperationFactory.CreateOperation(tokenizer.CurrentToken.Type);

                if (operation == null) return left;

                tokenizer.NextToken();

                var right = ParseLowPriority(tokenizer);

                left = new BinaryExpression(left, right, operation);
            }
        }

        private IExpression ParseLowPriority(ITokenizer tokenizer)
        {
            var left = ParseUnary(tokenizer);

            tokenizer.CurrentToken.EnsureNotUnknownTokenType();

            while (true)
            {
                var operation = OperationFactory.CreateOperation(tokenizer.CurrentToken.Type);

                if (operation == null) return left;

                tokenizer.NextToken();

                var right = ParseUnary(tokenizer);

                left = new BinaryExpression(left, right, operation);
            }
        }

        private IExpression ParseUnary(ITokenizer tokenizer)
        {
            while (true)
            {
                if (tokenizer.CurrentToken.Type == TokenType.Add)
                {
                    tokenizer.NextToken();

                    continue;
                }

                if (tokenizer.CurrentToken.Type == TokenType.Subtract)
                {
                    tokenizer.NextToken();

                    var right = ParseUnary(tokenizer);

                    return new UnaryExpression(right, a => -a);
                }

                return ParseLeaf(tokenizer);
            }
        }

        private IExpression ParseLeaf(ITokenizer tokenizer)
        {
            if (tokenizer.CurrentToken.Type == TokenType.Number)
            {
                var leaf = new NumberExpression(tokenizer.CurrentToken.Value);

                tokenizer.NextToken();

                return leaf;
            }

            if (tokenizer.CurrentToken.Type == TokenType.LeftBracket)
            {
                tokenizer.NextToken();

                var leaf = ParseHighPriority(tokenizer);

                tokenizer.CurrentToken.EnsureRightBracketTokenType();

                tokenizer.NextToken();

                return leaf;
            }

            throw new MathExpressionParseException($"Unexpected token: {tokenizer.CurrentToken.Type}");
        }
    }
}
