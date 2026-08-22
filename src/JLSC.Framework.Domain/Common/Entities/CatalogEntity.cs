namespace JLSC.Framework.Domain.Common.Entities;

public abstract class CatalogEntity : AuditableEntity
{
    protected CatalogEntity()
    {
    }

    /// <summary>
    /// Nombre del registro.
    /// </summary>
    public string Nombre { get; protected set; } = string.Empty;

    /// <summary>
    /// Descripción opcional.
    /// </summary>
    public string? Descripcion { get; protected set; }

    /// <summary>
    /// Orden de visualización.
    /// </summary>
    public int Orden { get; protected set; }

    public virtual void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

        Nombre = nombre.Trim();
    }

    public virtual void CambiarDescripcion(string? descripcion)
    {
        Descripcion = string.IsNullOrWhiteSpace(descripcion)
            ? null
            : descripcion.Trim();
    }

    public virtual void CambiarOrden(int orden)
    {
        if (orden < 0)
            throw new ArgumentOutOfRangeException(nameof(orden));

        Orden = orden;
    }
}