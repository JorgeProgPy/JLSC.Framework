using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerBanners;

public sealed class ObtenerBannersHandler
    : IUseCase<ObtenerBannersRequest, List<ObtenerBannersResponse>>
{
    private readonly IBannerRepository _bannerRepository;

    public ObtenerBannersHandler(
        IBannerRepository bannerRepository)
    {
        ArgumentNullException.ThrowIfNull(bannerRepository);

        _bannerRepository = bannerRepository;
    }

    public async Task<OperationResult<List<ObtenerBannersResponse>>> ExecuteAsync(
        ObtenerBannersRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var banners = await _bannerRepository.GetAllAsync(
            request.IncluirInactivos,
            cancellationToken);

        var response = banners
            .Select(banner => new ObtenerBannersResponse
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
            })
            .ToList();

        return OperationResult<List<ObtenerBannersResponse>>.SuccessResult(
            response);
    }
}