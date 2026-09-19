using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;


namespace JLSC.Framework.Application.Core.Portal.Commands.CrearBanner;

public sealed class CrearBannerHandler
    : IUseCase<CrearBannerRequest, CrearBannerResponse>
{
    private readonly IBannerRepository _bannerRepository;

    public CrearBannerHandler(IBannerRepository bannerRepository)
    {
        ArgumentNullException.ThrowIfNull(bannerRepository);

        _bannerRepository = bannerRepository;
    }

    public async Task<OperationResult<CrearBannerResponse>> ExecuteAsync(
        CrearBannerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Titulo))
        {
            return OperationResult<CrearBannerResponse>.Failure(
                "El título del banner es obligatorio.");
        }

        if (request.Orden < 0)
        {
            return OperationResult<CrearBannerResponse>.Failure(
                "El orden del banner no puede ser negativo.");
        }

        if (request.FechaInicio.HasValue &&
            request.FechaFin.HasValue &&
            request.FechaFin < request.FechaInicio)
        {
            return OperationResult<CrearBannerResponse>.Failure(
                "La fecha de fin no puede ser anterior a la fecha de inicio.");
        }

        Banner banner;

        try
        {
            banner = new Banner(
                request.Titulo,
                request.Subtitulo,
                request.ArchivoId,
                request.Enlace,
                request.Orden,
                request.FechaInicio,
                request.FechaFin);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<CrearBannerResponse>.Failure(ex.Message);
        }

        _bannerRepository.Add(banner);

        await _bannerRepository.GuardarCambiosAsync(cancellationToken);

        var response = new CrearBannerResponse
        {
            BannerId = banner.Id,
            PublicId = banner.PublicId,
            Titulo = banner.Titulo,
            Subtitulo = banner.Subtitulo,
            ArchivoId = banner.ArchivoId,
            Enlace = banner.Enlace,
            Orden = banner.Orden,
            FechaInicio = banner.FechaInicio,
            FechaFin = banner.FechaFin,
            Activo = banner.Activo
        };

        return OperationResult<CrearBannerResponse>.SuccessResult(response);
    }
}