using JLSC.Framework.Domain.Common.Entities;

namespace JLSC.Framework.Domain.Core.Catalogos.Entities;

public class CatalogoItem : CatalogEntity
{
    protected CatalogoItem()
    {
    }

    // ==========================
    // Relaciones
    // ==========================

    public long CatalogoId { get; private set; }

    // ==========================
    // Datos del negocio
    // ==========================

    public string? Valor { get; private set; }

    // ==========================
    // Presentación
    // ==========================

    public string? Icono { get; private set; }

    public string? Color { get; private set; }

    public string? CssClass { get; private set; }

    // ==========================
    // Configuración
    // ==========================

    public string? UrlBase { get; private set; }

    public string? Target { get; private set; }

    // ==========================
    // Navegación
    // ==========================

    public virtual Catalogo Catalogo { get; private set; } = null!;

    // ==========================
    // Fábrica
    // ==========================

    public static CatalogoItem Create(
        long catalogoId,
        string codigo,
        string nombre,
        string? valor,
        string? descripcion,
        string? icono,
        int orden)
    {
        if (catalogoId <= 0)
            throw new ArgumentOutOfRangeException(nameof(catalogoId));

        var item = new CatalogoItem();

        item.CatalogoId = catalogoId;

        item.AsignarCodigo(codigo);

        item.CambiarNombre(nombre);

        item.CambiarDescripcion(descripcion);

        item.CambiarOrden(orden);

        item.CambiarValor(valor);

        item.CambiarIcono(icono);

        item.Activar();

        return item;
    }

    // ==========================
    // Comportamiento
    // ==========================

    public void CambiarValor(string? valor)
    {
        Valor = string.IsNullOrWhiteSpace(valor)
            ? null
            : valor.Trim();
    }

    public void CambiarIcono(string? icono)
    {
        Icono = string.IsNullOrWhiteSpace(icono)
            ? null
            : icono.Trim();
    }

    public void CambiarColor(string? color)
    {
        Color = string.IsNullOrWhiteSpace(color)
            ? null
            : color.Trim();
    }

    public void CambiarCssClass(string? cssClass)
    {
        CssClass = string.IsNullOrWhiteSpace(cssClass)
            ? null
            : cssClass.Trim();
    }

    public void CambiarUrlBase(string? urlBase)
    {
        UrlBase = string.IsNullOrWhiteSpace(urlBase)
            ? null
            : urlBase.Trim();
    }

    public void CambiarTarget(string? target)
    {
        Target = string.IsNullOrWhiteSpace(target)
            ? null
            : target.Trim();
    }
}