using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Geografia.Entities;

public class Ubicacion : AuditableEntity
{
    // ==========================
    // Relaciones
    // ==========================

    public long PaisId { get; set; }

    public long DepartamentoId { get; set; }

    public long? ProvinciaId { get; set; }

    public long? CiudadId { get; set; }

    // ==========================
    // Dirección
    // ==========================

    public string? Urbanizacion { get; set; }

    public string? Barrio { get; set; }

    public string? Zona { get; set; }

    public string? Avenida { get; set; }

    public string? Calle { get; set; }

    public string? NumeroPuerta { get; set; }

    public string? Edificio { get; set; }

    public string? Piso { get; set; }

    public string? Oficina { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Referencia { get; set; }

    // ==========================
    // Geolocalización
    // ==========================

    public double? Latitud { get; set; }

    public double? Longitud { get; set; }

    // ==========================
    // Navegación
    // ==========================

    public virtual Pais Pais { get; set; } = null!;

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual Provincia? Provincia { get; set; }

    public virtual Ciudad? Ciudad { get; set; }
}