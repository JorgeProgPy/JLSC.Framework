using JLSC.Framework.Domain.Common.Exceptions;
using JLSC.Framework.Domain.Common.Helpers;

namespace JLSC.Framework.Domain.Common.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        PublicId = Guid.NewGuid();
    }

    /// <summary>
    /// Identificador interno de la Base de Datos.
    /// </summary>
    public long Id { get; protected set; }

    /// <summary>
    /// Identificador público para APIs e integraciones.
    /// </summary>
    public Guid PublicId { get; protected set; }

    /// <summary>
    /// Código funcional de la entidad.
    /// </summary>
    public string Codigo { get; protected set; } = string.Empty;

    public virtual void AsignarCodigo(string codigo)
    {
        codigo = TextNormalizer.NormalizeCode(codigo);

        if (string.IsNullOrWhiteSpace(codigo))
            throw new DomainValidationException(
                "El código es obligatorio.");

        Codigo = codigo;
    }
}