using CashFlow.Application.Interfaces;
using Moq;

namespace CashFlow.UnitTests.Services;

public class CashFlowServiceTest
{
    IMock<ICashTransactionRepository> _transactionRepositoryMock;

    public CashFlowServiceTest()
    {
        _transactionRepositoryMock = new Mock<ICashTransactionRepository>();
    }
    [Fact]
    public async Task AddCashTransaction_ShouldReturnSuccessResult()
    {
        // Arrange
        
        // Act

        // Assert
    }
}
