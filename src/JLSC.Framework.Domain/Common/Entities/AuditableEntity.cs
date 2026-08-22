namespace JLSC.Framework.Domain.Common.Entities;

public abstract class AuditableEntity : BaseEntity
{
    protected AuditableEntity()
    {
        Activo = true;
        FechaCreacion = DateTime.UtcNow;
    }

    /// <summary>
    /// Indica si el registro está activo.
    /// </summary>
    public bool Activo { get; protected set; }

    /// <summary>
    /// Fecha de creación.
    /// </summary>
    public DateTime FechaCreacion { get; protected set; }

    /// <summary>
    /// Fecha de modificación.
    /// </summary>
    public DateTime? FechaModificacion { get; protected set; }

    /// <summary>
    /// Usuario que creó el registro.
    /// </summary>
    public long? UsuarioCreacionId { get; protected set; }

    /// <summary>
    /// Usuario que modificó el registro.
    /// </summary>
    public long? UsuarioModificacionId { get; protected set; }

    public virtual void Activar()
    {
        Activo = true;
    }

    public virtual void Desactivar()
    {
        Activo = false;
    }

    public virtual void RegistrarCreacion(long? usuarioId)
    {
        UsuarioCreacionId = usuarioId;
        FechaCreacion = DateTime.UtcNow;
    }

    public virtual void RegistrarModificacion(long? usuarioId)
    {
        UsuarioModificacionId = usuarioId;
        FechaModificacion = DateTime.UtcNow;
    }
}