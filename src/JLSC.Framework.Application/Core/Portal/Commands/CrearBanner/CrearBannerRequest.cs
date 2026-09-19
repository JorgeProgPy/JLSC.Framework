namespace JLSC.Framework.Application.Core.Portal.Commands.CrearBanner;

public sealed class CrearBannerRequest
{
    public string Titulo { get; init; } = string.Empty;

    public string? Subtitulo { get; init; }

    public long? ArchivoId { get; init; }

    public string? Enlace { get; init; }

    public int Orden { get; init; }

    public DateTime? FechaInicio { get; init; }

    public DateTime? FechaFin { get; init; }
}