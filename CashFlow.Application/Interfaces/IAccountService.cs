using CashFlow.Application.Common;
using CashFlow.Application.DTOs.Account;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Interfaces;

public interface IAccountService
{
    Task<Result<UpdateAccountDto>> AddAccountAsync(CreateAccountDto account);
    Task<Result<GetAccountDto>> GetAccountByIdAsync(long accountId);
    Task<Result<bool>> DeleteAccountByIdAsync(long accountId);
    Task<Result<UpdateAccountDto>> UpdateAccount(UpdateAccountDto account);
    Task<IReadOnlyList<GetAccountDto>> GetAllAccountsAsync();
}
