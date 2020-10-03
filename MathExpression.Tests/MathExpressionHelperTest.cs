using FluentAssertions;
using I2M.MathExpression.Helpers;
using Xunit;

namespace I2M.MathExpression.Tests
{
    public class MathExpressionHelperTest
    {
        [Fact]
        public void Parse_ExpressionString_ReturnsExpectedEval()
        {
            // Arrange
            const string expressionString = "-((10 + 20) - (5 + 5) / 2 * 2)";

            // Act
            var result = MathExpressionHelper.Parse(expressionString).Eval();

            // Assert
            result.Should().Be(-20);
        }
    }
}
