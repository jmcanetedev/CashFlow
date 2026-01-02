using CashFlow.Application.Interfaces;
using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Persistence.Repositories.CashTransaction;

public class CashTransactionRepository : ICashTransactionRepository
{
    private readonly AppDbContext _context;
    public CashTransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.CashTransaction> AddCashTransactionAsync(Domain.Entities.CashTransaction cashTransaction)
    {
        _context.Transactions.Add(cashTransaction);
        
        await  _context.SaveChangesAsync();

        return cashTransaction;
    }

    public async Task<bool> DeleteCashTransactionAsync(long id)
    {
        var exist = await _context.Transactions.FindAsync(id);
        if(exist == null)
            return false;

        exist.DeletedAt = DateTime.UtcNow;

        _context.Transactions.Update(exist);

        var result =  await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<IReadOnlyList<Domain.Entities.CashTransaction>> GetAllCashTransactionsAsync()
    {
        var transactions = await _context.Transactions.AsNoTracking().OrderByDescending(c=>c.Id).ToListAsync();

        return transactions;
    }

    public async Task<Domain.Entities.CashTransaction> GetCashTransactionByIdAsync(long id)
    {
        var exist =  await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(t=> t.Id == id);
        return exist;
    }

    public async Task<IReadOnlyList<Domain.Entities.CashTransaction>> GetCashTransactionByRangeAsync(DateTime start, DateTime end)
    {
       

        var transactions =  await _context.Transactions
            .AsNoTracking()
            .Where(t => t.TransactionDate >= start && t.TransactionDate < end).OrderByDescending(c => c.Id)
            .ToListAsync();

        return transactions;
    }

    public async Task<Domain.Entities.CashTransaction> UpdateCashTransactionAsync(Domain.Entities.CashTransaction cashTransaction)
    {
        var exist = await _context.Transactions.FindAsync(cashTransaction.Id);

        if (exist == null)
            throw new Exception("CashTransaction not found");

        _context.Entry(exist).CurrentValues.SetValues(cashTransaction);

        await _context.SaveChangesAsync();
        
        return cashTransaction;
    }
}
