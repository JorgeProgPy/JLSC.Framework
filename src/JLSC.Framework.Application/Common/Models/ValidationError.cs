namespace JLSC.Framework.Application.Common.Models;

public sealed class ValidationError
{
    public ValidationError(
        string propertyName,
        string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    public string PropertyName { get; }

    public string ErrorMessage { get; }
}