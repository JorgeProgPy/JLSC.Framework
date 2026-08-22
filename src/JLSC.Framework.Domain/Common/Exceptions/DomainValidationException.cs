namespace JLSC.Framework.Domain.Common.Exceptions;

public sealed class DomainValidationException : DomainException
{
    public DomainValidationException(string message)
        : base(message)
    {
    }
}