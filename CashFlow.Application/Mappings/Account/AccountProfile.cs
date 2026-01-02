using AutoMapper;

namespace CashFlow.Application.Mappings.Account;

public class AccountProfile : Profile
{
    public AccountProfile()
    {

        CreateMap<Domain.Entities.Account, DTOs.Account.GetAccountDto>();
        CreateMap<Domain.Entities.Account, DTOs.Account.CreateAccountDto>();
        CreateMap<Domain.Entities.Account, DTOs.Account.UpdateAccountDto>();
    }
}
