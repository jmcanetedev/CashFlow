using CashFlow.Domain.Enums;

namespace CashFlow.Application.DTOs.Account;

public class GetAccountDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public AccountType AccountType { get; set; } = AccountType.Savings;
    public Guid UserId { get; set; }
}
