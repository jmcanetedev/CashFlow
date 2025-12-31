using CashFlow.Application.Interfaces;
using CashFlow.Application.Mappings.CashTransaction;
using CashFlow.Application.Services.CashTransaction;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class ConfigureServices
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICashTransactionService, CashTransactionService>();
        services.AddAutoMapper(opt=>
        {
            opt.AddMaps(typeof(CashTransactionProfile).Assembly);
        });
    }
}