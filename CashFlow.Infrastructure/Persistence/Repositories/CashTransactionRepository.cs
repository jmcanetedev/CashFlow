using CashFlow.Application.Common;
using CashFlow.Application.Interfaces;
using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Persistence.Repositories;

public class CashTransactionRepository : ICashTransactionRepository
{
    private readonly AppDbContext _context;
    public CashTransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CashTransaction> AddCashTransaction(CashTransaction cashTransaction)
    {
        _context.Transactions.Add(cashTransaction);
        
        await  _context.SaveChangesAsync();

        return cashTransaction;
    }

    public async Task<bool> DeleteCashTransaction(long id)
    {
        var exist = await _context.Transactions.FindAsync(id);
        if(exist == null)
            return false;
        return true;
    }

    public async Task<IReadOnlyList<CashTransaction>> GetAllCashTransactions()
    {
        var transactions = await _context.Transactions.AsNoTracking().OrderByDescending(c=>c.Id).ToListAsync();

        return transactions;
    }

    public async Task<CashTransaction> GetCashTransactionById(long id)
    {
        var exist =  await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(t=> t.Id == id);
        return exist;
    }

    public async Task<IReadOnlyList<CashTransaction>> GetCashTransactionByRange(DateTime start, DateTime end)
    {
       

        var transactions =  await _context.Transactions
            .AsNoTracking()
            .Where(t => t.TransactionDate >= start && t.TransactionDate < end).OrderByDescending(c => c.Id)
            .ToListAsync();

        return transactions;
    }

    public async Task<CashTransaction> UpdateCashTransaction(CashTransaction cashTransaction)
    {
        var exist = await _context.Transactions.FindAsync(cashTransaction.Id);

        if (exist == null)
            throw new Exception("CashTransaction not found");

        _context.Entry(exist).CurrentValues.SetValues(cashTransaction);

        await _context.SaveChangesAsync();
        
        return cashTransaction;
    }
}
