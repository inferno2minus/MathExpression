using FluentAssertions;
using I2M.MathExpression.Interfaces;
using I2M.MathExpression.Tokenizers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace I2M.MathExpression.Tests
{
    public class MathExpressionEngineTests
    {
        [Fact]
        public void ParseExpression_ValidExpression_ReturnsParsedResult()
        {
            // Arrange
            var tokens = new Queue<Token>(new[]
            {
                // -((10 + 20) - 5 * 5 + 2 * 2)
                new Token(new[] {'-'}),
                new Token(new[] {'('}),
                new Token(new[] {'('}),
                new Token(new[] {'1', '0'}, 10),
                new Token(new[] {'+'}),
                new Token(new[] {'2', '0'}, 20),
                new Token(new[] {')'}),
                new Token(new[] {'-'}),
                new Token(new[] {'5'}, 5),
                new Token(new[] {'*'}),
                new Token(new[] {'5'}, 5),
                new Token(new[] {'+'}),
                new Token(new[] {'2'}, 2),
                new Token(new[] {'*'}),
                new Token(new[] {'2'}, 2),
                new Token(new[] {')'}),
                new Token(new[] {'\0'})
            });

            var tokenizerMock = new Mock<ITokenizer>();
            var operationFactoryMock = new Mock<IOperationFactory>();
            var engine = new MathExpressionEngine(operationFactoryMock.Object);

            Token token = null;

            tokenizerMock.Setup(x => x.Init()).Callback(() => token = tokens.Dequeue());
            tokenizerMock.Setup(x => x.NextToken()).Callback(() => token = tokens.Dequeue());
            tokenizerMock.SetupGet(x => x.CurrentToken).Returns(() => token);

            operationFactoryMock.Setup(x => x.CreateLowPriorityOperation('+')).Returns((a, b) => a + b);
            operationFactoryMock.Setup(x => x.CreateLowPriorityOperation('-')).Returns((a, b) => a - b);
            operationFactoryMock.Setup(x => x.CreateHighPriorityOperation('*')).Returns((a, b) => a * b);

            // Act
            var result = engine.ParseExpression(tokenizerMock.Object).Eval();

            // Assert
            result.Should().Be(-9);
        }
    }
}
