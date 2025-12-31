using CashFlow.Application.Common;
using CashFlow.Application.DTOs.CashTransaction;

namespace CashFlow.Application.Interfaces;

public interface ICashTransactionService
{
    Task<Result<UpdateCashTransactionDto>> AddCashTransaction(CreateCashTransactionDto dto);
    Task<Result<UpdateCashTransactionDto>> UpdateCashTransaction(UpdateCashTransactionDto dto);
    Task<Result<bool>> DeleteCashTransaction(long id);
    Task<Result<GetCashTransactionDto>> GetCashTransactionById(long id);
    Task<Result<IReadOnlyList<GetCashTransactionDto>>> GetAllCashTransactions();
    Task<Result<IReadOnlyList<CashFlowSliceDto>>> GetCashTransactionByRange(CashFlowFilterDto filter);
    Task<Result<IncomeExpenseReportDto>> GetIncomeExpenseReport(CashFlowFilterDto filter);
    Task<Result<SavingReportDto>> GetSavingReport(CashFlowFilterDto filter);
}
