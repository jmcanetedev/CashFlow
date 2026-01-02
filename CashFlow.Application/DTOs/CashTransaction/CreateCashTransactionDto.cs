using CashFlow.Domain.Enums;

namespace CashFlow.Application.DTOs.CashTransaction;

public sealed class CreateCashTransactionDto
{
    public decimal Amount { get; set; }
    public long AccountId { get; set; }
    public TransactionType Type { get; set; } = TransactionType.Income;
    public string Description { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
