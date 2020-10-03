using FluentAssertions;
using I2M.MathExpression.Operations;
using Xunit;

namespace I2M.MathExpression.Tests
{
    public class OperationFactoryTest
    {
        [Fact]
        public void CreateHighPriorityOperation_DivideSymbol_ReturnsExpectedOperation()
        {
            // Arrange
            const char symbol = '/';

            var operationFactory = new OperationFactory();

            // Act
            var operation = operationFactory.CreateHighPriorityOperation(symbol);

            // Assert
            operation.Should().NotBeNull();
            operation.Invoke(10, 2).Should().Be(5);
        }

        [Fact]
        public void CreateHighPriorityOperation_MultiplySymbol_ReturnsExpectedOperation()
        {
            // Arrange
            const char symbol = '*';

            var operationFactory = new OperationFactory();

            // Act
            var operation = operationFactory.CreateHighPriorityOperation(symbol);

            // Assert
            operation.Should().NotBeNull();
            operation.Invoke(10, 2).Should().Be(20);
        }

        [Fact]
        public void CreateLowPriorityOperation_AddSymbol_ReturnsExpectedOperation()
        {
            // Arrange
            const char symbol = '+';

            var operationFactory = new OperationFactory();

            // Act
            var operation = operationFactory.CreateLowPriorityOperation(symbol);

            // Assert
            operation.Should().NotBeNull();
            operation.Invoke(10, 2).Should().Be(12);
        }

        [Fact]
        public void CreateLowPriorityOperation_SubtractSymbol_ReturnsExpectedOperation()
        {
            // Arrange
            const char symbol = '-';

            var operationFactory = new OperationFactory();

            // Act
            var operation = operationFactory.CreateLowPriorityOperation(symbol);

            // Assert
            operation.Should().NotBeNull();
            operation.Invoke(10, 2).Should().Be(8);
        }
    }
}
