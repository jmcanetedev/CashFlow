using System.Security.Claims;

namespace CashFlow.Web.Security;

public sealed class CurrentUser : ICurrentUser
{
    public Guid UserId { get; private set; }
    public long CurrentAccountId { get; private set; }


    public string Avatar { get; private set; }
    public CurrentUser(IHttpContextAccessor accessor)
    {
        var id = accessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier);

        UserId = id is null
            ? Guid.Empty
            : Guid.Parse(id);
        var name = accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        Avatar = name.Length > 0 ? name[0].ToString().ToUpper() : "";
    }
    public bool IsAuthenticated => UserId != Guid.Empty;

    public void SetCurrentAccountId(long accountId)
    {
        CurrentAccountId = accountId;
    }
}
