using JLSC.Framework.Domain.Core.Catalogos.Entities;

namespace JLSC.Framework.Domain.Core.Portal.Entities;

public sealed class RedSocialInstitucional
{
    public const int MaxUrlLength = 500;

    public long Id { get; private set; }
    public Guid PublicId { get; private set; }

    public long ConfiguracionInstitucionalId { get; private set; }
    public long CatalogoItemId { get; private set; }

    public string Url { get; private set; } = null!;
    public int Orden { get; private set; }
    public bool Activo { get; private set; }

    public DateTime FechaCreacion { get; private set; }
    public DateTime? FechaModificacion { get; private set; }

    public ConfiguracionInstitucional ConfiguracionInstitucional { get; private set; } = null!;
    public CatalogoItem CatalogoItem { get; private set; } = null!;

    private RedSocialInstitucional()
    {
    }

    public RedSocialInstitucional(
        long configuracionInstitucionalId,
        long catalogoItemId,
        string url,
        int orden = 0)
    {
        if (configuracionInstitucionalId <= 0)
            throw new ArgumentOutOfRangeException(nameof(configuracionInstitucionalId));

        if (catalogoItemId <= 0)
            throw new ArgumentOutOfRangeException(nameof(catalogoItemId));

        Url = NormalizarUrl(url);

        if (Url.Length > MaxUrlLength)
            throw new ArgumentException(
                $"La URL de la red social no puede superar los {MaxUrlLength} caracteres.");

        if (orden < 0)
            throw new ArgumentException(
                "El orden de la red social no puede ser negativo.");

        Id = 0;
        PublicId = Guid.NewGuid();

        ConfiguracionInstitucionalId = configuracionInstitucionalId;
        CatalogoItemId = catalogoItemId;
        Orden = orden;

        Activo = true;
        FechaCreacion = DateTime.UtcNow;
    }

    public void Actualizar(
        long catalogoItemId,
        string url,
        int orden)
    {
        if (catalogoItemId <= 0)
            throw new ArgumentOutOfRangeException(nameof(catalogoItemId));

        Url = NormalizarUrl(url);

        if (Url.Length > MaxUrlLength)
            throw new ArgumentException(
                $"La URL de la red social no puede superar los {MaxUrlLength} caracteres.");

        if (orden < 0)
            throw new ArgumentException(
                "El orden de la red social no puede ser negativo.");

        CatalogoItemId = catalogoItemId;
        Orden = orden;

        FechaModificacion = DateTime.UtcNow;
    }

    public void Activar()
    {
        Activo = true;
        FechaModificacion = DateTime.UtcNow;
    }

    public void Desactivar()
    {
        Activo = false;
        FechaModificacion = DateTime.UtcNow;
    }

    private static string NormalizarUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException(
                "La URL de la red social es obligatoria.");

        return url.Trim();
    }
}