namespace Application.Core;

public class ResultValidator<T>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Value { get; set; }

    public static ResultValidator<T> Success(T value, int statusCode) => new ResultValidator<T> { IsSuccess = true, Value = value, StatusCode = statusCode };

    public static ResultValidator<T> Error(string error, int statusCode) => new ResultValidator<T> { IsSuccess = false, Message = error, StatusCode = statusCode };
}



