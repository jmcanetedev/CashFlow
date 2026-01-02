using CashFlow.Application.Interfaces;
using CashFlow.Infrastructure.Persistence;
using CashFlow.Infrastructure.Persistence.Repositories.Account;
using CashFlow.Infrastructure.Persistence.Repositories.CashTransaction;
using CashFlow.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICashTransactionRepository, CashTransactionRepository>();
        services.AddScoped<IAuthService, IdentityAuthService>();

    }
}
