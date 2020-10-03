using FluentAssertions;
using I2M.MathExpression.Tokenizers;
using System.IO;
using Xunit;

namespace I2M.MathExpression.Tests
{
    public class TokenizerTest
    {
        [Fact]
        public void NextToken_SymbolsString_ReturnsExpectedSymbol()
        {
            // Arrange
            const string symbolsString = "- +";

            using var reader = new StringReader(symbolsString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();

            // Assert
            tokenizer.CurrentToken.Symbol.Should().Be('-');
            tokenizer.NextToken();
            tokenizer.CurrentToken.Symbol.Should().Be('+');
        }

        [Fact]
        public void NextToken_IntegerNumbersString_ReturnsExpectedValue()
        {
            // Arrange
            const string integerNumbersString = "10 20";

            using var reader = new StringReader(integerNumbersString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();

            // Assert
            tokenizer.CurrentToken.Value.Should().Be(10);
            tokenizer.NextToken();
            tokenizer.CurrentToken.Value.Should().Be(20);
        }

        [Fact]
        public void NextToken_DoubleNumbersString_ReturnsExpectedValue()
        {
            // Arrange
            const string doubleNumbersString = "10.5 .5";

            using var reader = new StringReader(doubleNumbersString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();

            // Assert
            tokenizer.CurrentToken.Value.Should().Be(10.5);
            tokenizer.NextToken();
            tokenizer.CurrentToken.Value.Should().Be(0.5);
        }
    }
}
