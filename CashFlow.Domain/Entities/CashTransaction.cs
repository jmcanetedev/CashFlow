using CashFlow.Domain.Common;
using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities;

public class CashTransaction : BaseEntity
{
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public DateTime? TransactionDate { get; private set; }
    public CashTransaction() { } // For EF Core

    private CashTransaction(
        decimal amount,
        TransactionType type,
        string name,
        string description,
        DateTime? transactionDate)
    {
        Amount = amount;
        Type = type;
        Name = name;
        Description = description;
        TransactionDate = transactionDate;
    }
    public static DomainResult<CashTransaction> Create(
        decimal amount,
        TransactionType type,
        string name,
        string description,
        DateTime? transactionDate)
    {
        if (amount <= 0)
            return DomainResult<CashTransaction>.Failure("Amount must be greater than zero.");
        if (string.IsNullOrWhiteSpace(name))
            return DomainResult<CashTransaction>.Failure("Name cannot be null or empty.");

        var transaction = new CashTransaction(
            amount,
            type,
            name,
            description,
            transactionDate);

        return DomainResult<CashTransaction>.Success(transaction);
    
    }
    public DomainResult<CashTransaction> Modify(
        decimal amount,
        TransactionType type,
        string name,
        string description,
        DateTime? transactionDate)
    {
        if (amount <= 0)
            return DomainResult<CashTransaction>.Failure("Amount must be greater than zero.");
        if (string.IsNullOrWhiteSpace(name))
            return DomainResult<CashTransaction>.Failure("Name cannot be null or empty.");

        Amount = amount;
        Type = type;
        Name = name;
        Description = description;
        TransactionDate = transactionDate;
        
        return DomainResult<CashTransaction>.Success(this);
    }
}
