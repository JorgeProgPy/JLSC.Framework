using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActivarBanner;

public sealed class ActivarBannerHandler
    : IUseCase<ActivarBannerRequest, ActivarBannerResponse>
{
    private readonly IBannerRepository _bannerRepository;

    public ActivarBannerHandler(
        IBannerRepository bannerRepository)
    {
        ArgumentNullException.ThrowIfNull(bannerRepository);

        _bannerRepository = bannerRepository;
    }

    public async Task<OperationResult<ActivarBannerResponse>> ExecuteAsync(
        ActivarBannerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.BannerId <= 0)
        {
            return OperationResult<ActivarBannerResponse>.Failure(
                "El identificador del banner no es válido.");
        }

        var banner = await _bannerRepository.GetByIdAsync(
            request.BannerId,
            cancellationToken);

        if (banner is null)
        {
            return OperationResult<ActivarBannerResponse>.Failure(
                "El banner indicado no existe.");
        }

        banner.Activar();

        _bannerRepository.Update(banner);

        await _bannerRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new ActivarBannerResponse
        {
            BannerId = banner.Id,
            PublicId = banner.PublicId,
            Titulo = banner.Titulo,
            Activo = banner.Activo
        };

        return OperationResult<ActivarBannerResponse>.SuccessResult(
            response);
    }
}