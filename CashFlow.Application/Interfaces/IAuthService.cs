using CashFlow.Application.DTOs.Auth;

namespace CashFlow.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterDto request);
    Task<AuthResultDto> LoginAsync(LoginDto request);
    Task LogoutAsync();
}
