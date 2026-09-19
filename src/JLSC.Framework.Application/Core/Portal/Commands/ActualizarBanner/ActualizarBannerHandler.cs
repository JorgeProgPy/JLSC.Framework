using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarBanner;

public sealed class ActualizarBannerHandler
    : IUseCase<ActualizarBannerRequest, ActualizarBannerResponse>
{
    private readonly IBannerRepository _bannerRepository;

    public ActualizarBannerHandler(
        IBannerRepository bannerRepository)
    {
        ArgumentNullException.ThrowIfNull(bannerRepository);

        _bannerRepository = bannerRepository;
    }

    public async Task<OperationResult<ActualizarBannerResponse>> ExecuteAsync(
        ActualizarBannerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.BannerId <= 0)
        {
            return OperationResult<ActualizarBannerResponse>.Failure(
                "El identificador del banner no es válido.");
        }

        if (string.IsNullOrWhiteSpace(request.Titulo))
        {
            return OperationResult<ActualizarBannerResponse>.Failure(
                "El título del banner es obligatorio.");
        }

        if (request.Orden < 0)
        {
            return OperationResult<ActualizarBannerResponse>.Failure(
                "El orden del banner no puede ser negativo.");
        }

        if (request.FechaInicio.HasValue &&
            request.FechaFin.HasValue &&
            request.FechaFin < request.FechaInicio)
        {
            return OperationResult<ActualizarBannerResponse>.Failure(
                "La fecha de fin no puede ser anterior a la fecha de inicio.");
        }

        var banner = await _bannerRepository.GetByIdAsync(
            request.BannerId,
            cancellationToken);

        if (banner is null)
        {
            return OperationResult<ActualizarBannerResponse>.Failure(
                "El banner indicado no existe.");
        }

        try
        {
            banner.Actualizar(
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
            return OperationResult<ActualizarBannerResponse>.Failure(
                ex.Message);
        }

        _bannerRepository.Update(banner);

        await _bannerRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new ActualizarBannerResponse
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

        return OperationResult<ActualizarBannerResponse>.SuccessResult(
            response);
    }
}