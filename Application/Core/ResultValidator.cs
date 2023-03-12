namespace Application.Core;

public class ResultValidator<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Value { get; set; }

    public static ResultValidator<T> Success(T value) => new ResultValidator<T> { IsSuccess = true, Value = value };

    public static ResultValidator<T> Error(string error) => new ResultValidator<T> { IsSuccess = false, Message = error };
}



