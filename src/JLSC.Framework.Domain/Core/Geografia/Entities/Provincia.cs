using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Geografia.Entities;

public class Provincia : AuditableEntity
{
    public long DepartamentoId { get; set; }



    public string Nombre { get; set; } = null!;

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();
}