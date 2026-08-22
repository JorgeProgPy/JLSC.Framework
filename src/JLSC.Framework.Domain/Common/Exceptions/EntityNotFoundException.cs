namespace JLSC.Framework.Domain.Common.Exceptions;

public sealed class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(
        string entityName,
        object key)
        : base($"{entityName} '{key}' no fue encontrado.")
    {
    }
}