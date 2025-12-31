using CashFlow.Domain.Enums;

namespace CashFlow.Application.DTOs.CashTransaction;

public sealed class UpdateCashTransactionDto
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? TransactionDate { get; set; }
}
