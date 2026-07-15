using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Catalogos.Entities;

public class Catalogo : CatalogEntity
{
    public bool EsSistema { get; set; }

    public virtual ICollection<CatalogoItem> Items { get; set; } = new List<CatalogoItem>();
}