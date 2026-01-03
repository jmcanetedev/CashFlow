namespace CashFlow.Web.Security;

public interface ICurrentUser
{
    Guid UserId { get; }
    string Avatar { get; }
    long CurrentAccountId { get; }
    bool IsAuthenticated { get; }
    void SetCurrentAccountId(long accountId);
}
