namespace JLSC.Framework.Web.ViewModels.Portal;

public sealed class InicioPortalViewModel
{
    public List<BannerViewModel> Banners { get; init; } = [];
    public List<IndicadorInstitucionalViewModel> Indicadores { get; init; } = [];
    public List<AccesoRapidoViewModel> AccesosRapidos { get; init; } = [];
    public List<ProyectoDestacadoViewModel> ProyectosDestacados { get; init; } = [];
}

public sealed class BannerViewModel
{
    public long Id { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string? Subtitulo { get; init; }
    public long? ArchivoId { get; init; }
    public string? Enlace { get; init; }
    public int Orden { get; init; }
}

public sealed class IndicadorInstitucionalViewModel
{
    public long Id { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string Valor { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Icono { get; init; }
    public int Orden { get; init; }
}

public sealed class AccesoRapidoViewModel
{
    public long Id { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Icono { get; init; }
    public string Enlace { get; init; } = string.Empty;
    public int Orden { get; init; }
}

public sealed class ProyectoDestacadoViewModel
{
    public long Id { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Ubicacion { get; init; }
    public long? ArchivoId { get; init; }
    public string? Enlace { get; init; }
    public int Orden { get; init; }
}