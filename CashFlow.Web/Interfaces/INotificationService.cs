namespace CashFlow.Web.Interfaces;

public interface INotificationService
{
    void Success(string message);
    void Error(string message);
    void Warning(string message);
}
