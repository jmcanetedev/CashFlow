using CashFlow.Domain.Entities;

namespace CashFlow.Application.Interfaces;

public interface IAccountRepository
{
    Task<Account> AddAccountAsync(Account account);
    Task<Account> GetAccountByIdAsync(long accountId);
    Task<bool> DeleteAccountByIdAsync(long accountId);
    Task<Account> UpdateAccountAsync(Account account);
    Task<IReadOnlyList<Account>> GetAllAccountsAsync();
    Task<IReadOnlyList<Account>> GetAccountByUserIdAsync(Guid userId);
}
