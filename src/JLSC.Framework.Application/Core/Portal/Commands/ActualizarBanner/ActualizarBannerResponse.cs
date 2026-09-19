namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarBanner;

public sealed class ActualizarBannerResponse
{
    public long BannerId { get; init; }

    public Guid PublicId { get; init; }

    public string Titulo { get; init; } = string.Empty;

    public string? Subtitulo { get; init; }

    public long? ArchivoId { get; init; }

    public string? Enlace { get; init; }

    public int Orden { get; init; }

    public DateTime? FechaInicio { get; init; }

    public DateTime? FechaFin { get; init; }

    public bool Activo { get; init; }
}