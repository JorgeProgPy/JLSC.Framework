using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Core.Catalogos.Entities;

namespace JLSC.Framework.Domain.Core.Contactos.Entities;

/// <summary>
/// Representa un medio de contacto individual.
/// Ejemplo: Teléfono, WhatsApp, Correo, Facebook, Sitio Web.
/// </summary>
public class ContactoItem : AuditableEntity
{
    // ==========================
    // Relaciones
    // ==========================

    public long ContactoId { get; set; }

    public long TipoContactoId { get; set; }

    // ==========================
    // Datos
    // ==========================

    public string Valor { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    // ==========================
    // Configuración
    // ==========================

    public bool EsPrincipal { get; set; }

    public bool VisiblePortal { get; set; } = true;

    public bool VisibleDirectorio { get; set; } = true;

    public bool Verificado { get; set; }

    public int Orden { get; set; }

    // ==========================
    // Navegación
    // ==========================

    public virtual Contacto Contacto { get; set; } = null!;

    public virtual CatalogoItem TipoContacto { get; set; } = null!;
}