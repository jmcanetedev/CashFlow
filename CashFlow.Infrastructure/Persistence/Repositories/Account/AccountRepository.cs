using CashFlow.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Persistence.Repositories.Account;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Domain.Entities.Account> AddAccountAsync(Domain.Entities.Account account)
    {
        _context.Accounts.Add(account);

        await  _context.SaveChangesAsync();

        return account;
    }

    public async Task<bool> DeleteAccountByIdAsync(long accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);

        if (account == null) return false;
        
        account.DeletedAt = DateTime.UtcNow;

        _context.Accounts.Update(account);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Domain.Entities.Account> GetAccountByIdAsync(long accountId)
    {
        var exist = await _context.Accounts.FindAsync(accountId);

        return exist;
    }

    public async Task<IReadOnlyList<Domain.Entities.Account>> GetAccountByUserIdAsync(Guid userId)
    {
        var userAccounts = await _context.Accounts.AsNoTracking().Where(c=>c.UserId == userId).OrderByDescending(c=>c.Id).ToListAsync();
        return userAccounts;
    }

    public async Task<IReadOnlyList<Domain.Entities.Account>> GetAllAccountsAsync()
    {
       var accounts = await _context.Accounts.AsNoTracking().OrderByDescending(a=>a.Id).ToListAsync();

       return accounts;
    }

    public async Task<Domain.Entities.Account> UpdateAccountAsync(Domain.Entities.Account account)
    {
        var exist = await _context.Accounts.FindAsync(account.Id);
        if(exist == null)
            throw new KeyNotFoundException($"Account with Id {account.Id} not found.");

        _context.Entry(exist).CurrentValues.SetValues(account);

        await _context.SaveChangesAsync();

        return exist;
    }
}
