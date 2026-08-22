namespace JLSC.Framework.Domain.Common.Exceptions;

public sealed class DuplicateEntityException : DomainException
{
    public DuplicateEntityException(
        string entityName,
        string value)
        : base($"Ya existe un registro de '{entityName}' con el valor '{value}'.")
    {
    }
}