using AutoMapper;
using CashFlow.Application.DTOs.CashTransaction;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Mappings.CashTransaction;

public class CashTransactionProfile : Profile
{
   public CashTransactionProfile()
   {
        CreateMap<Domain.Entities.CashTransaction, GetCashTransactionDto>();
        CreateMap<Domain.Entities.CashTransaction, UpdateCashTransactionDto>();
    }
}
