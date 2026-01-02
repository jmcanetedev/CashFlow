using CashFlow.Application.Common;
using CashFlow.Application.DTOs.CashTransaction;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Interfaces;

public interface ICashTransactionRepository
{
    Task<CashTransaction> AddCashTransactionAsync(CashTransaction cashTransaction);
    Task<CashTransaction> UpdateCashTransactionAsync(CashTransaction cashTransaction);
    Task<bool> DeleteCashTransactionAsync(long id);
    Task<CashTransaction> GetCashTransactionByIdAsync(long id);
    Task<IReadOnlyList<CashTransaction>> GetAllCashTransactionsAsync();
    Task<IReadOnlyList<CashTransaction>> GetCashTransactionByRangeAsync(DateTime start, DateTime end);
}
