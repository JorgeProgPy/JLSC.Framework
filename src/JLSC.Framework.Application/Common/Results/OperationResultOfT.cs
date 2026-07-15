namespace JLSC.Framework.Application.Common.Results;

public class OperationResult<T> : OperationResult
{
    private OperationResult(
        bool success,
        T? data,
        string? message)
        : base(success, message)
    {
        Data = data;
    }

    public T? Data { get; }

    public static OperationResult<T> SuccessResult(
        T data,
        string? message = null)
    {
        return new OperationResult<T>(
            true,
            data,
            message);
    }

    public static new OperationResult<T> Failure(
        string message)
    {
        return new OperationResult<T>(
            false,
            default,
            message);
    }
}