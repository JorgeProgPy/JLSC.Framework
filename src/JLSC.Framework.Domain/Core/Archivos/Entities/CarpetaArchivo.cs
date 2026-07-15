using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Archivos.Entities;

public class CarpetaArchivo : CatalogEntity
{
    // ==========================
    // Jerarquía
    // ==========================

    public long? CarpetaPadreId { get; set; }

    // ==========================
    // Configuración Visual
    // ==========================

    public string? Icono { get; set; }

    public string? CssClass { get; set; }

    // ==========================
    // Configuración
    // ==========================

    public bool EsRaiz { get; set; }

    public bool Visible { get; set; } = true;

    public string? Descripcion { get; set; }

    // ==========================
    // Navegación
    // ==========================

    public virtual CarpetaArchivo? CarpetaPadre { get; set; }

    public virtual ICollection<CarpetaArchivo> Subcarpetas { get; set; }
        = new List<CarpetaArchivo>();

    public virtual ICollection<Archivo> Archivos { get; set; }
        = new List<Archivo>();
}