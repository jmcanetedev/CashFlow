using CashFlow.Domain.Enums;

namespace CashFlow.Application.DTOs.Account;

public class CreateAccountDto
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public AccountType AccountType { get; set; } = AccountType.Savings;
    public bool IsDefault { get; set; } = false;
}
