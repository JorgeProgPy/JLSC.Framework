namespace JLSC.Framework.Domain.Common.Exceptions;

/// <summary>
/// Representa una violación de una regla de negocio.
/// </summary>
public class BusinessException : DomainException
{
    public BusinessException(string message)
        : base(message)
    {
    }
}