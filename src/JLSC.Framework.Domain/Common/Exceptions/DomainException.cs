namespace JLSC.Framework.Domain.Common.Exceptions;

/// <summary>
/// Excepción base para todas las reglas del dominio.
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message)
        : base(message)
    {
    }

    protected DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}