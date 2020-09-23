using FluentAssertions;
using I2M.MathExpression.Exceptions;
using I2M.MathExpression.Helpers;
using System;
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
        [InlineData("1 + 1 * 10", 11)]
        [InlineData("10.5 + 10.5", 21)]
        [InlineData("10 * 20 / 2", 100)]
        [InlineData("10 - 20 * 2", -30)]
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

        [Theory]
        [InlineData("10)", "Unexpected characters at end of expression")]
        [InlineData("!10", "Unexpected token: Unknown")]
        [InlineData("10 ! 10", "Unexpected token: Unknown")]
        [InlineData("10 * 20 ! 30", "Unexpected token: Unknown")]
        [InlineData("10 - (20 ! 2)", "Unexpected token: Unknown")]
        [InlineData("(10 - 20) ! 2", "Unexpected token: Unknown")]
        [InlineData("10 - (20 + 1", "Missing closing bracket")]

        public void ParseExpression_InvalidExpression_ThrowsMathExpressionParseException(string value, string expected)
        {
            // Act
            Action result = () => MathExpressionHelper.Parse(value);

            // Assert
            result.Should().Throw<ExpressionParseException>().WithMessage(expected);
        }
    }
}
