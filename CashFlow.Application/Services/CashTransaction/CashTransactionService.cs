using AutoMapper;
using CashFlow.Application.Common;
using CashFlow.Application.DTOs.CashTransaction;
using CashFlow.Application.Interfaces;
using CashFlow.Domain.Enums;

namespace CashFlow.Application.Services.CashTransaction;

internal class CashTransactionService : ICashTransactionService
{
    private readonly ICashTransactionRepository _repository;
    private readonly IMapper _mapper;
    public CashTransactionService(ICashTransactionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Result<UpdateCashTransactionDto>> AddCashTransaction(CreateCashTransactionDto dto)
    {
        var cashTransaction = Domain.Entities.CashTransaction.Create(
            dto.Amount,
            dto.Type,
            dto.Name,
            dto.Description,
            DateTime.UtcNow
        );

        if(!cashTransaction.IsSuccess)
        {
            return Result<UpdateCashTransactionDto>.Failure(cashTransaction.ErrorMessage);
        }

       var result = await _repository.AddCashTransaction(cashTransaction.Data);

        return Result<UpdateCashTransactionDto>.Success(_mapper.Map<UpdateCashTransactionDto>(result));
    }

    public async Task<Result<bool>> DeleteCashTransaction(long id)
    {
        var exist = await _repository.DeleteCashTransaction(id);

        if (!exist)
        {
            return Result<bool>.Failure("Cash transaction not found.");
        }

        return Result<bool>.Success(exist);
    }

    public async Task<Result<IReadOnlyList<GetCashTransactionDto>>> GetAllCashTransactions()
    {
        var transactions = await _repository.GetAllCashTransactions();
        
        var mappedTransactions = _mapper.Map<IReadOnlyList<GetCashTransactionDto>>(transactions);

        return Result<IReadOnlyList<GetCashTransactionDto>>.Success(mappedTransactions);
    }

    public async Task<Result<GetCashTransactionDto>> GetCashTransactionById(long id)
    {
        var exist = await _repository.GetCashTransactionById(id);
        if (exist == null)
        {
            return Result<GetCashTransactionDto>.Failure("Cash transaction not found.");
        }
        var mappedTransaction = _mapper.Map<GetCashTransactionDto>(exist);

        return Result<GetCashTransactionDto>.Success(mappedTransaction);
    }

    public async Task<Result<IReadOnlyList<CashFlowSliceDto>>> GetCashTransactionByRange(CashFlowFilterDto filter)
    {
        var transactions = await _repository.GetCashTransactionByRange(filter.StartDate, filter.EndDate);

        var groupedTransactions = transactions.GroupBy(transactions => transactions.Type)
            .Select(group => new CashFlowSliceDto
            {
                Type = group.Key,
                TotalAmount = group.Sum(transaction => transaction.Amount)
            })
            .ToList();

        return Result<IReadOnlyList<CashFlowSliceDto>>.Success(groupedTransactions);
    }

    public async Task<Result<IncomeExpenseReportDto>> GetIncomeExpenseReport(CashFlowFilterDto filter)
    {
        var transactions = await _repository.GetCashTransactionByRange(filter.StartDate, filter.EndDate);

        var incomes = transactions
            .Where(x => x.Type == TransactionType.Income)
            .Select(x => new CashTransactionReportItemDto
            {
                Name = x.Name,
                Description = x.Description,
                TransactionDate = x.TransactionDate.Value,
                Amount = x.Amount
            })
            .ToList();

        var expenses = transactions
            .Where(x => x.Type == TransactionType.Expense)
            .Select(x => new CashTransactionReportItemDto
            {
                Name = x.Name,
                Description = x.Description,
                TransactionDate = x.TransactionDate.Value,
                Amount = x.Amount
            })
            .ToList();

        return Result<IncomeExpenseReportDto>.Success(new IncomeExpenseReportDto { Incomes = incomes, Expenses = expenses, TotalExpenses = expenses.Sum(x => x.Amount), TotalIncome = incomes.Sum(x => x.Amount) });

    }

    public async Task<Result<SavingReportDto>> GetSavingReport(CashFlowFilterDto filter)
    {
        var transactions = await _repository.GetCashTransactionByRange(filter.StartDate, filter.EndDate);

        var savings = transactions
            .Where(x => x.Type == TransactionType.Saving)
            .Select(x => new CashTransactionReportItemDto
            {
                Name = x.Name,
                Description = x.Description,
                TransactionDate = x.TransactionDate.Value,
                Amount = x.Amount
            })
            .ToList();

        return Result<SavingReportDto>.Success(new SavingReportDto { Savings = savings, TotalSavings = savings.Sum(x => x.Amount) });
    }

    public async Task<Result<UpdateCashTransactionDto>> UpdateCashTransaction(UpdateCashTransactionDto dto)
    {
        var exist = await _repository.GetCashTransactionById(dto.Id);

        var modifiedTransaction = exist.Modify(dto.Amount, dto.Type, dto.Name, dto.Description, dto.TransactionDate);

        if (!modifiedTransaction.IsSuccess)
        {
            return Result<UpdateCashTransactionDto>.Failure(modifiedTransaction.ErrorMessage);
        }

        var updatedTransaction = await _repository.UpdateCashTransaction(modifiedTransaction.Data);

        return Result<UpdateCashTransactionDto>.Success(_mapper.Map<UpdateCashTransactionDto>(updatedTransaction));
    }
}
