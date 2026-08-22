using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Catalogos.Entities;

public class Catalogo : CatalogEntity
{
    protected Catalogo()
    {
    }

    public bool EsSistema { get; private set; }

    public virtual ICollection<CatalogoItem> Items { get; private set; }
        = new List<CatalogoItem>();

    public static Catalogo Create(
        string codigo,
        string nombre,
        bool esSistema)
    {
        var catalogo = new Catalogo();

        catalogo.AsignarCodigo(codigo);
        catalogo.CambiarNombre(nombre);

        catalogo.EsSistema = esSistema;

        catalogo.Activar();

        return catalogo;
    }

    public void CambiarEsSistema(bool esSistema)
    {
        EsSistema = esSistema;
    }
}