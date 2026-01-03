namespace CashFlow.Application.DTOs.CashTransaction;

public sealed class CashFlowFilterDto
{
    public long CurrentAccountId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
