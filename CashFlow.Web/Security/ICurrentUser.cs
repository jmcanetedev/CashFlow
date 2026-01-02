namespace CashFlow.Web.Security;

public interface ICurrentUser
{
    Guid UserId { get; }
}
