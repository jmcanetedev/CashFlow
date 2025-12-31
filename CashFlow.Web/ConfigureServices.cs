using CashFlow.Web.Interfaces;
using CashFlow.Web.Services;

namespace CashFlow.Web;

public static class ConfigureServices
{
    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
    }
}
