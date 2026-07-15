using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Geografia.Entities;

public class Pais : AuditableEntity
{
    public string Codigo { get; set; } = null!;

    public string CodigoISO2 { get; set; } = null!;

    public string CodigoISO3 { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Nacionalidad { get; set; } = null!;

    public string? CodigoTelefonico { get; set; }

    public string? DominioInternet { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();
}