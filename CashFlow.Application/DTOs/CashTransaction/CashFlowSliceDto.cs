using CashFlow.Domain.Enums;

namespace CashFlow.Application.DTOs.CashTransaction;

public sealed class CashFlowSliceDto
{
   public TransactionType Type { get; set; }
   public decimal TotalAmount { get; set; } 
}
