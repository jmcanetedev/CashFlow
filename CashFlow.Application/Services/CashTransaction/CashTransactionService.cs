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
    public async Task<Result<UpdateCashTransactionDto>> AddCashTransactionAsync(CreateCashTransactionDto dto)
    {
        var cashTransaction = Domain.Entities.CashTransaction.Create(dto.AccountId,
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

       var result = await _repository.AddCashTransactionAsync(cashTransaction.Data);

        return Result<UpdateCashTransactionDto>.Success(_mapper.Map<UpdateCashTransactionDto>(result));
    }

    public async Task<Result<bool>> DeleteCashTransactionAsync(long id)
    {
        var exist = await _repository.DeleteCashTransactionAsync(id);

        if (!exist)
        {
            return Result<bool>.Failure("Cash transaction not found.");
        }

        return Result<bool>.Success(exist);
    }

    public async Task<Result<IReadOnlyList<GetCashTransactionDto>>> GetAllCashTransactionsAsync(long accountId)
    {
        var transactions = await _repository.GetAllCashTransactionsAsync(accountId);
        
        var mappedTransactions = _mapper.Map<IReadOnlyList<GetCashTransactionDto>>(transactions);

        return Result<IReadOnlyList<GetCashTransactionDto>>.Success(mappedTransactions);
    }

    public async Task<Result<GetCashTransactionDto>> GetCashTransactionByIdAsync(long id)
    {
        var exist = await _repository.GetCashTransactionByIdAsync(id);
        if (exist == null)
        {
            return Result<GetCashTransactionDto>.Failure("Cash transaction not found.");
        }
        var mappedTransaction = _mapper.Map<GetCashTransactionDto>(exist);

        return Result<GetCashTransactionDto>.Success(mappedTransaction);
    }

    public async Task<Result<IReadOnlyList<CashFlowSliceDto>>> GetCashTransactionByRangeAsync(CashFlowFilterDto filter)
    {
        var from = filter.StartDate.Date;
        var to = filter.EndDate.Date.AddDays(1);

        var transactions = await _repository.GetCashTransactionByRangeAsync(filter.CurrentAccountId, from, to);

        var groupedTransactions = transactions.GroupBy(transactions => transactions.Type)
            .Select(group => new CashFlowSliceDto
            {
                Type = group.Key,
                TotalAmount = group.Sum(transaction => transaction.Amount)
            })
            .ToList();

        return Result<IReadOnlyList<CashFlowSliceDto>>.Success(groupedTransactions);
    }

    public async Task<Result<IncomeExpenseReportDto>> GetIncomeExpenseReportAsync(CashFlowFilterDto filter)
    {
        var from = filter.StartDate.Date;
        var to = filter.EndDate.Date.AddDays(1);

        var transactions = await _repository.GetCashTransactionByRangeAsync(filter.CurrentAccountId, from, to);

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

    public async Task<Result<SavingReportDto>> GetSavingReportAsync(CashFlowFilterDto filter)
    {
        var from = filter.StartDate.Date;
        var to = filter.EndDate.Date.AddDays(1);

        var transactions = await _repository.GetCashTransactionByRangeAsync(filter.CurrentAccountId, from, to);

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

    public async Task<Result<UpdateCashTransactionDto>> UpdateCashTransactionAsync(UpdateCashTransactionDto dto)
    {
        var exist = await _repository.GetCashTransactionByIdAsync(dto.Id);

        if (exist == null)
            return Result<UpdateCashTransactionDto>.Failure("Cash transaction not found.");

        var modifiedTransaction = exist.Modify(dto.Amount, dto.Type, dto.Name, dto.Description, dto.TransactionDate);

        if (!modifiedTransaction.IsSuccess)
        {
            return Result<UpdateCashTransactionDto>.Failure(modifiedTransaction.ErrorMessage);
        }

        var updatedTransaction = await _repository.UpdateCashTransactionAsync(exist);

        return Result<UpdateCashTransactionDto>.Success(_mapper.Map<UpdateCashTransactionDto>(updatedTransaction));
    }
}
