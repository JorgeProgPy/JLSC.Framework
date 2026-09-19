namespace JLSC.Framework.Application.Core.Portal.Commands.ActivarBanner;

public sealed class ActivarBannerResponse
{
    public long BannerId { get; init; }

    public Guid PublicId { get; init; }

    public string Titulo { get; init; } = string.Empty;

    public bool Activo { get; init; }
}