using CashFlow.Application.Common;
using CashFlow.Application.DTOs.CashTransaction;

namespace CashFlow.Application.Interfaces;

public interface ICashTransactionService
{
    Task<Result<UpdateCashTransactionDto>> AddCashTransactionAsync(CreateCashTransactionDto dto);
    Task<Result<UpdateCashTransactionDto>> UpdateCashTransactionAsync(UpdateCashTransactionDto dto);
    Task<Result<bool>> DeleteCashTransactionAsync(long id);
    Task<Result<GetCashTransactionDto>> GetCashTransactionByIdAsync(long id);
    Task<Result<IReadOnlyList<GetCashTransactionDto>>> GetAllCashTransactionsAsync(long accountId);
    Task<Result<IReadOnlyList<CashFlowSliceDto>>> GetCashTransactionByRangeAsync(CashFlowFilterDto filter);
    Task<Result<IncomeExpenseReportDto>> GetIncomeExpenseReportAsync(CashFlowFilterDto filter);
    Task<Result<SavingReportDto>> GetSavingReportAsync(CashFlowFilterDto filter);
}
