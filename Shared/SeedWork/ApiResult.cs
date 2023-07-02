namespace Shared.SeedWork;

public class ApiResult<T>
{
    public bool IsSucceeded { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    
    public ApiResult() { }

    public ApiResult(bool isSucceeded, string message = null) => (IsSucceeded, Message) = (isSucceeded, message);
    
    public ApiResult(bool isSucceeded, T data, string message = null) => (IsSucceeded, Data, Message) = (isSucceeded, data, message);
}