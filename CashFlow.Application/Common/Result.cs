using System.Text;

namespace CashFlow.Application.Common;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }

    public static Result Success()
    {
        return new Result
        {
            IsSuccess = true
        };
    }
    public static Result Failure(string errorMessage)
    {
        return new Result
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}
public class Result<T> : Result
{
    public T Data { get; set; }
    public static Result<T> Success(T data)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data
        };
    }
    public static new Result<T> Failure(string errorMessage)
    {
        return new Result<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}
