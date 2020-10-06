using FluentAssertions;
using I2M.MathExpression.Tokenizers;
using System.IO;
using System.Linq;
using Xunit;

namespace I2M.MathExpression.Tests
{
    public class TokenizerTest
    {
        [Fact]
        public void Init_SymbolString_ReturnsSymbol()
        {
            // Arrange
            const string symbolString = "-";

            using var reader = new StringReader(symbolString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();

            // Assert
            tokenizer.CurrentToken.Symbols.First().Should().Be('-');
        }

        [Fact]
        public void Init_IntegerNumberString_ReturnsIntegerNumber()
        {
            // Arrange
            const string integerNumberString = "10";

            using var reader = new StringReader(integerNumberString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();

            // Assert
            tokenizer.CurrentToken.Number.Should().Be(10);
        }

        [Fact]
        public void Init_DoubleNumberString_ReturnsDoubleNumber()
        {
            // Arrange
            const string doubleNumbersString = "10.5";

            using var reader = new StringReader(doubleNumbersString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();

            // Assert
            tokenizer.CurrentToken.Number.Should().Be(10.5);
        }

        [Fact]
        public void NextToken_SymbolsString_ReturnsSymbol()
        {
            // Arrange
            const string symbolsString = "- +";

            using var reader = new StringReader(symbolsString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();
            tokenizer.NextToken();

            // Assert
            tokenizer.CurrentToken.Symbols.First().Should().Be('+');
        }

        [Fact]
        public void NextToken_IntegerNumbersString_ReturnsIntegerNumber()
        {
            // Arrange
            const string integerNumbersString = "10 20";

            using var reader = new StringReader(integerNumbersString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();
            tokenizer.NextToken();

            // Assert
            tokenizer.CurrentToken.Number.Should().Be(20);
        }

        [Fact]
        public void NextToken_DoubleNumbersString_ReturnsDoubleNumber()
        {
            // Arrange
            const string doubleNumbersString = "10.5 .5";

            using var reader = new StringReader(doubleNumbersString);
            var tokenizer = new Tokenizer(reader);

            // Act
            tokenizer.Init();
            tokenizer.NextToken();

            // Assert
            tokenizer.CurrentToken.Number.Should().Be(0.5);
        }
    }
}
