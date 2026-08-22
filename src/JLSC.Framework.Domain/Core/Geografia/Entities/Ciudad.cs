using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Geografia.Entities;

public class Ciudad : AuditableEntity
{
    public long ProvinciaId { get; set; }



    public string Nombre { get; set; } = null!;

    public virtual Provincia Provincia { get; set; } = null!;
}