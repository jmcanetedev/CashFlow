using System.Security.Claims;

namespace CashFlow.Web.Security;

public class CurrentUser : ICurrentUser
{
    public Guid UserId { get; private set; }
    public CurrentUser(IHttpContextAccessor accessor)
    {
        var id = accessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier);

        UserId = id is null
            ? Guid.Empty
            : Guid.Parse(id);
    }
}
