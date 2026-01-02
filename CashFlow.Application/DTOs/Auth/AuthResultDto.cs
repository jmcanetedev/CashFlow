namespace CashFlow.Application.DTOs.Auth;

public class AuthResultDto
{
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; } = new();
}
