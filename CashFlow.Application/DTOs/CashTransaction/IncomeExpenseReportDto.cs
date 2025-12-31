namespace CashFlow.Application.DTOs.CashTransaction;

public sealed class IncomeExpenseReportDto
{
    public IReadOnlyList<CashTransactionReportItemDto> Incomes { get; set; }
    public IReadOnlyList<CashTransactionReportItemDto> Expenses { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal Net => TotalIncome - TotalExpenses;
}
