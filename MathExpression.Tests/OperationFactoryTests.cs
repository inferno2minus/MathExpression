using FluentAssertions;
using I2M.MathExpression.Operations;
using Xunit;

namespace I2M.MathExpression.Tests
{
    public class OperationFactoryTests
    {
        [Fact]
        public void CreateHighPriorityOperation_DivideSymbol_ReturnsDivideOperation()
        {
            // Arrange
            const char divideSymbol = '/';

            var operationFactory = new OperationFactory();

            // Act
            var operation = operationFactory.CreateHighPriorityOperation(divideSymbol);

            // Assert
            operation.Should().NotBeNull();
            operation.Invoke(10, 2).Should().Be(5);
        }

        [Fact]
        public void CreateHighPriorityOperation_MultiplySymbol_ReturnsMultiplyOperation()
        {
            // Arrange
            const char multiplySymbol = '*';

            var operationFactory = new OperationFactory();

            // Act
            var operation = operationFactory.CreateHighPriorityOperation(multiplySymbol);

            // Assert
            operation.Should().NotBeNull();
            operation.Invoke(10, 2).Should().Be(20);
        }

        [Fact]
        public void CreateLowPriorityOperation_AddSymbol_ReturnsAddOperation()
        {
            // Arrange
            const char addSymbol = '+';

            var operationFactory = new OperationFactory();

            // Act
            var operation = operationFactory.CreateLowPriorityOperation(addSymbol);

            // Assert
            operation.Should().NotBeNull();
            operation.Invoke(10, 2).Should().Be(12);
        }

        [Fact]
        public void CreateLowPriorityOperation_SubtractSymbol_ReturnsSubtractOperation()
        {
            // Arrange
            const char subtractSymbol = '-';

            var operationFactory = new OperationFactory();

            // Act
            var operation = operationFactory.CreateLowPriorityOperation(subtractSymbol);

            // Assert
            operation.Should().NotBeNull();
            operation.Invoke(10, 2).Should().Be(8);
        }
    }
}
