using AutoMapper;
using CashFlow.Application.Common;
using CashFlow.Application.DTOs.Account;
using CashFlow.Application.Interfaces;

namespace CashFlow.Application.Services.Account;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }
    public async Task<Result<UpdateAccountDto>> AddAccountAsync(CreateAccountDto account)
    {
        var accountResult = Domain.Entities.Account.Create(account.Name, account.UserId, account.AccountType);

        if (!accountResult.IsSuccess)
            return Result<UpdateAccountDto>.Failure(accountResult.ErrorMessage);

        var createdAccount = await _accountRepository.AddAccountAsync(accountResult.Data);

        return Result<UpdateAccountDto>.Success(_mapper.Map<UpdateAccountDto>(createdAccount));
    }

    public async Task<Result<bool>> DeleteAccountByIdAsync(long accountId)
    {
        var exist = await _accountRepository.DeleteAccountByIdAsync(accountId);
        if(!exist)
            return Result<bool>.Failure("Account not found");

        return Result<bool>.Success(exist);
    }

    public async Task<Result<GetAccountDto>> GetAccountByIdAsync(long accountId)
    {
        var exist  = await _accountRepository.GetAccountByIdAsync(accountId);
        if (exist == null)
            return Result<GetAccountDto>.Failure("Account not found");

        var mappedAccount = _mapper.Map<GetAccountDto>(exist);

        return Result<GetAccountDto>.Success(mappedAccount);
    }

    public async Task<IReadOnlyList<GetAccountDto>> GetAllAccountsAsync()
    {
        var accounts =  await _accountRepository.GetAllAccountsAsync();

        var mappedAccounts = _mapper.Map<IReadOnlyList<GetAccountDto>>(accounts);

        return mappedAccounts;
    }

    public async Task<Result<UpdateAccountDto>> UpdateAccount(UpdateAccountDto account)
    {
        var exist = await _accountRepository.GetAccountByIdAsync(account.Id);
        if (exist == null)
            return Result<UpdateAccountDto>.Failure("Account not found");

        var accountResult = exist.Modify(account.Name);

        if(!accountResult.IsSuccess)
            return Result<UpdateAccountDto>.Failure(accountResult.ErrorMessage);

        await _accountRepository.UpdateAccountAsync(exist);

        return Result<UpdateAccountDto>.Success(_mapper.Map<UpdateAccountDto>(exist));
    }
}
