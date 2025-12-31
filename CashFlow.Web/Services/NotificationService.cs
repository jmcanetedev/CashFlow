using CashFlow.Web.Interfaces;
using MudBlazor;

namespace CashFlow.Web.Services;

public class NotificationService : INotificationService
{
    private readonly ISnackbar _snackbar;
    public NotificationService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
        _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopRight;
    }
    public void Error(string message) =>
        _snackbar.Add(message, Severity.Error);

    public void Success(string message) =>
        _snackbar.Add(message, Severity.Success);

    public void Warning(string message) =>
        _snackbar.Add(message, Severity.Warning);   
}
