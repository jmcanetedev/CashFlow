using CashFlow.Infrastructure.Identity;
using CashFlow.Infrastructure.Persistence;
using CashFlow.Web.Interfaces;
using CashFlow.Web.Security;
using CashFlow.Web.Services;
using Microsoft.AspNetCore.Identity;

namespace CashFlow.Web;

public static class ConfigureServices
{
    public static void AddNotificationService(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
    }
    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
        
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/login";
            options.AccessDeniedPath = "/error";
            options.Cookie.Name = ".CashFlow.Auth";
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        });
    }
    public static void AddCashFlowHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("Api", client =>
        {
            var baseApiUrl = configuration["BaseUrl"];
            client.BaseAddress = new Uri($"{baseApiUrl}/api/");
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            UseCookies = true
        });

        services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));
    }
}
