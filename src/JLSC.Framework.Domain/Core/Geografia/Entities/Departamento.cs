using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Geografia.Entities;

public class Departamento : AuditableEntity
{
    public long PaisId { get; set; }

 

    public string Nombre { get; set; } = null!;

    public virtual Pais Pais { get; set; } = null!;

    public virtual ICollection<Provincia> Provincias { get; set; } = new List<Provincia>();

}