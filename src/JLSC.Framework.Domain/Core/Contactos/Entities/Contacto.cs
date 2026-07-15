using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Contactos.Entities;

/// <summary>
/// Representa el conjunto de medios de contacto de una entidad.
/// Puede ser utilizado por organizaciones, personas, empresas,
/// sucursales y cualquier otro módulo del Framework.
/// </summary>
public class Contacto : AuditableEntity
{
    // ==========================
    // Navegación
    // ==========================

    public virtual ICollection<ContactoItem> Items { get; set; }
        = new List<ContactoItem>();
}