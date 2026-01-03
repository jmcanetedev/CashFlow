using CashFlow.Domain.Entities;

namespace CashFlow.Application.Interfaces;

public interface ICashTransactionRepository
{
    Task<CashTransaction> AddCashTransactionAsync(CashTransaction cashTransaction);
    Task<CashTransaction> UpdateCashTransactionAsync(CashTransaction cashTransaction);
    Task<bool> DeleteCashTransactionAsync(long id);
    Task<CashTransaction> GetCashTransactionByIdAsync(long id);
    Task<IReadOnlyList<CashTransaction>> GetAllCashTransactionsAsync(long accountId);
    Task<IReadOnlyList<CashTransaction>> GetCashTransactionByRangeAsync(long accountId, DateTime start, DateTime end);
}
