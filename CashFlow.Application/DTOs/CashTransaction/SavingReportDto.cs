namespace CashFlow.Application.DTOs.CashTransaction;

public sealed class SavingReportDto
{
    public IReadOnlyList<CashTransactionReportItemDto> Savings { get; set; }
    public decimal TotalSavings { get; set; }
}
