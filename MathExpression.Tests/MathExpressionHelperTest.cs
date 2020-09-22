using FluentAssertions;
using I2M.MathExpression.Helpers;
using Xunit;

namespace I2M.MathExpression.Tests
{
    public class MathExpressionHelperTest
    {
        [Theory]
        [InlineData("+10", 10)]
        [InlineData("-10", -10)]
        [InlineData("10 + 20", 30)]
        [InlineData("10 - 20", -10)]
        [InlineData("10 + 20 + 30", 60)]
        [InlineData("10 * 20", 200)]
        [InlineData("10 / 20", 0.5)]
        [InlineData("10.5 + 10.5", 21)]
        [InlineData("10 * 20 / 2", 100)]
        [InlineData("(10 * 20) / 2", 100)]
        [InlineData("10 + (20 / 2)", 20)]
        [InlineData("10 - (20 / 2)", 0)]
        [InlineData("-(10 + (20 / 2))", -20)]
        [InlineData("-((10 + 20) / 2)", -15)]
        public void ParseExpression_ValidExpression_ReturnsExpectedResult(string value, double expected)
        {
            // Arrange
            var expression = MathExpressionHelper.Parse(value);

            // Act
            var result = expression.Eval();

            // Assert
            result.Should().Be(expected);
        }
    }
}
