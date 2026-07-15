namespace JLSC.Framework.Domain.Common.Entities;

public abstract class AuditableEntity : BaseEntity
{
    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaModificacion { get; set; }

    public long? UsuarioCreacionId { get; set; }

    public long? UsuarioModificacionId { get; set; }
}