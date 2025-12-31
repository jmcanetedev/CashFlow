using CashFlow.Application.Common;
using CashFlow.Application.DTOs.CashTransaction;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Interfaces;

public interface ICashTransactionRepository
{
    Task<CashTransaction> AddCashTransaction(CashTransaction cashTransaction);
    Task<CashTransaction> UpdateCashTransaction(CashTransaction cashTransaction);
    Task<bool> DeleteCashTransaction(long id);
    Task<CashTransaction> GetCashTransactionById(long id);
    Task<IReadOnlyList<CashTransaction>> GetAllCashTransactions();
    Task<IReadOnlyList<CashTransaction>> GetCashTransactionByRange(DateTime start, DateTime end);
}
