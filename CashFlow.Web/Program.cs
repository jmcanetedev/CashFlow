using CashFlow.Application;
using CashFlow.Infrastructure;
using CashFlow.Web;
using CashFlow.Web.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddMudServices();
// Add custom services
//Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration);
//Application Layer
builder.Services.AddApplication();
//Notification Service
builder.Services.AddNotificationService();
//Identity Service
builder.Services.AddIdentityServices();
//Http Client Service
builder.Services.AddCashFlowHttpClient(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.MapRazorPages();
app.MapControllers();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.UseStatusCodePages();

app.Run();
