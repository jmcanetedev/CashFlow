using CashFlow.Domain.Entities;
using CashFlow.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<CashTransaction> Transactions => Set<CashTransaction>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .IsRequired();

        modelBuilder.Entity<Account>()
            .HasMany<CashTransaction>()
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId);
    }
}
