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
        var accountResult = Domain.Entities.Account.Create(account.Name, account.UserId, account.AccountType, account.IsDefault);

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

    public async Task<Result<IReadOnlyList<GetAccountDto>>> GetAccountByUserIdAsync(Guid userId)
    {
        var accounts =  await _accountRepository.GetAccountByUserIdAsync(userId);
        return Result<IReadOnlyList<GetAccountDto>>.Success(_mapper.Map<IReadOnlyList<GetAccountDto>>(accounts));
    }

    public async Task<Result<IReadOnlyList<GetAccountDto>>> GetAllAccountsAsync()
    {
        var accounts =  await _accountRepository.GetAllAccountsAsync();
        return Result<IReadOnlyList<GetAccountDto>>.Success(_mapper.Map<IReadOnlyList<GetAccountDto>>(accounts));
    }

    public async Task<Result<UpdateAccountDto>> UpdateAccount(UpdateAccountDto account)
    {
        var exist = await _accountRepository.GetAccountByIdAsync(account.Id);
        if (exist == null)
            return Result<UpdateAccountDto>.Failure("Account not found");

        var accountResult = exist.Modify(account.Name, account.IsDefault);

        if(!accountResult.IsSuccess)
            return Result<UpdateAccountDto>.Failure(accountResult.ErrorMessage);

        await _accountRepository.UpdateAccountAsync(exist);

        return Result<UpdateAccountDto>.Success(_mapper.Map<UpdateAccountDto>(exist));
    }
    public async Task<Result<long?>> ResolveDefaultAccountIdAsync(Guid userId)
    {
        var accounts = await _accountRepository.GetAccountByUserIdAsync(userId);

        if (accounts.Count == 0)
            return null;

        // Rule 1: Explicit default
        var defaultAccount = accounts.FirstOrDefault(a => a.IsDefault);
        if (defaultAccount is not null)
            return Result<long?>.Success(defaultAccount.Id);

        // Rule 2: Single account fallback
        if (accounts.Count == 1)
            return Result<long?>.Success(accounts[0].Id);

        // Rule 3: No default → let UI ask user
        return Result<long?>.Failure("No Default Account");
    }

}
