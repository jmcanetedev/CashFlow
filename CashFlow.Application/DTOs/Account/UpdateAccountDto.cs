namespace CashFlow.Application.DTOs.Account;

public class UpdateAccountDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDefault { get; set; } = false;
}
