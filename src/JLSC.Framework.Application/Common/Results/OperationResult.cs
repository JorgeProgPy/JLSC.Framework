namespace JLSC.Framework.Application.Common.Results;

public class OperationResult
{
    protected OperationResult(
        bool success,
        string? message = null)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; }

    public string? Message { get; }

    public static OperationResult SuccessResult(
        string? message = null)
    {
        return new OperationResult(
            true,
            message);
    }

    public static OperationResult Failure(
        string message)
    {
        return new OperationResult(
            false,
            message);
    }
}