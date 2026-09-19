using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerBanner;

public sealed class ObtenerBannerHandler
    : IUseCase<ObtenerBannerRequest, ObtenerBannerResponse>
{
    private readonly IBannerRepository _bannerRepository;

    public ObtenerBannerHandler(
        IBannerRepository bannerRepository)
    {
        ArgumentNullException.ThrowIfNull(bannerRepository);

        _bannerRepository = bannerRepository;
    }

    public async Task<OperationResult<ObtenerBannerResponse>> ExecuteAsync(
        ObtenerBannerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.BannerId <= 0)
        {
            return OperationResult<ObtenerBannerResponse>.Failure(
                "El identificador del banner no es válido.");
        }

        var banner = await _bannerRepository.GetByIdAsync(
            request.BannerId,
            cancellationToken);

        if (banner is null)
        {
            return OperationResult<ObtenerBannerResponse>.Failure(
                "El banner indicado no existe.");
        }

        var response = new ObtenerBannerResponse
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

        return OperationResult<ObtenerBannerResponse>.SuccessResult(
            response);
    }
}