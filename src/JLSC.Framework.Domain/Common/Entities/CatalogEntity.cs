namespace JLSC.Framework.Domain.Common.Entities;

public abstract class CatalogEntity : AuditableEntity
{
    public string Codigo { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public short Orden { get; set; }
}