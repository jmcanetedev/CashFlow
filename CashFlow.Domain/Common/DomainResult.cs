namespace CashFlow.Domain.Common;

public class DomainResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }

    public static DomainResult Success()
    {
        return new DomainResult
        {
            IsSuccess = true
        };
    }
    public static DomainResult Failure(string errorMessage)
    {
        return new DomainResult
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}
public class DomainResult<T> : DomainResult
{
    public T? Data { get; set; }
    public static DomainResult<T> Success(T data)
    {
        return new DomainResult<T>
        {
            IsSuccess = true,
            Data = data
        };
    }
    public static new DomainResult<T> Failure(string errorMessage)
    {
        return new DomainResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}
