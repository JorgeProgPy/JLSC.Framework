using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Catalogos.Entities;

public class CatalogoItem : CatalogEntity
{
    // ==========================
    // Relaciones
    // ==========================

    public long CatalogoId { get; set; }

    // ==========================
    // Datos del negocio
    // ==========================

    public string? Valor { get; set; }

    public string? Descripcion { get; set; }

    // ==========================
    // Presentación (UI)
    // ==========================

    public string? Icono { get; set; }

    public string? Color { get; set; }

    public string? CssClass { get; set; }

    // ==========================
    // Comportamiento
    // ==========================

    public string? UrlBase { get; set; }

    public string? Target { get; set; }

    // ==========================
    // Sistema
    // ==========================

    public int Orden { get; set; }

    // ==========================
    // Navegación
    // ==========================

    public virtual Catalogo Catalogo { get; set; } = null!;
}