using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<CashTransaction> Transactions => Set<CashTransaction>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
