using CashFlow.Domain.Common;
using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities;

public class Account : BaseEntity
{
   public string Name { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
   public string AccountNumber { get; private set; } = string.Empty;
   public AccountType AccountType { get; private set; }
    private Account() { } // For EF Core

    private Account(string name, Guid userId, AccountType accountType)
    {
        Name = name;
        AccountType = accountType;
        UserId = userId;
    }

    public static DomainResult<Account> Create(string name, Guid userId, AccountType accountType)
    {
        if (!string.IsNullOrWhiteSpace(name))
            return DomainResult<Account>.Failure("Account name is required.");

        return DomainResult<Account>.Success(new Account(name, userId, accountType));
    }
    public DomainResult Modify(string name)
    {
        if (!string.IsNullOrEmpty(name))
            return DomainResult.Failure("Account name is required.");

        Name = name;
        return DomainResult.Success();
    }
}
