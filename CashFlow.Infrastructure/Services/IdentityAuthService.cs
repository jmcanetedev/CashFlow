using CashFlow.Application.DTOs.Auth;
using CashFlow.Application.Interfaces;
using CashFlow.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace CashFlow.Infrastructure.Services;

public class IdentityAuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityAuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthResultDto> RegisterAsync(RegisterDto request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return new AuthResultDto
            {
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };

        return new AuthResultDto
        {
            IsSuccess = true,
            Errors = Array.Empty<string>().ToList()
        };
    }

    public async Task<AuthResultDto> LoginAsync(LoginDto request)
    {
        var result = await _signInManager.PasswordSignInAsync(
            request.Email,
            request.Password,
            true,
            false);

        if (!result.Succeeded)
            return new AuthResultDto
            {
                IsSuccess = false,
                Errors = new List<string> { "Invalid login attempt." }
            };

        return new AuthResultDto
        {
            IsSuccess = true,
            Errors = Array.Empty<string>().ToList()
        };
    }

    public Task LogoutAsync()
        => _signInManager.SignOutAsync();
}
