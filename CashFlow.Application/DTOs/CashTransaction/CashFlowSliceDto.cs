using CashFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashFlow.Application.DTOs.CashTransaction;

public sealed class CashFlowSliceDto
{
   public TransactionType Type { get; set; }
   public decimal TotalAmount { get; set; } 
}
