namespace JLSC.Framework.Application.Common.Models;

public sealed class ErrorDetail
{
    public ErrorDetail(
        string code,
        string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }
}