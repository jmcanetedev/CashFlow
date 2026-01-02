using CashFlow.Application.DTOs.Auth;
using CashFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CashFlow.Web.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly IAuthService _authService;
    [BindProperty]
    public LoginDto Input { get; set; } = new();

    public LoginModel(IAuthService authService)
    {
        _authService = authService;
    }
    public void OnGet()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            Response.Redirect("/");
        }
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _authService.LoginAsync(Input);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return Page();
        }
        return LocalRedirect("/");
    }
}
