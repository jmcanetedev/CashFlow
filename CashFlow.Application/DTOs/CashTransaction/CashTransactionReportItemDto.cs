namespace CashFlow.Application.DTOs.CashTransaction;

public sealed class CashTransactionReportItemDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
}
