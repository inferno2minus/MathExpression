using FluentAssertions;
using I2M.MathExpression.Operations;
using I2M.MathExpression.Tokenizers;
using System.IO;
using Xunit;

namespace I2M.MathExpression.Tests
{
    public class MathExpressionEngineTest
    {
        [Fact]
        public void ParseExpression_ExpressionString_ReturnsExpectedEval()
        {
            // Arrange
            const string expressionString = "-((10 + 20) - 5 * 5 + 2 * 2)";

            using var reader = new StringReader(expressionString);

            var tokenizer = new Tokenizer(reader);

            var operationFactory = new OperationFactory();

            var engine = new MathExpressionEngine(operationFactory);

            // Act
            var result = engine.ParseExpression(tokenizer).Eval();

            // Assert
            result.Should().Be(-9);
        }
    }
}
