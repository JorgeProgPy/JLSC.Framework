using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.DesactivarBanner;

public sealed class DesactivarBannerHandler
    : IUseCase<DesactivarBannerRequest, DesactivarBannerResponse>
{
    private readonly IBannerRepository _bannerRepository;

    public DesactivarBannerHandler(
        IBannerRepository bannerRepository)
    {
        ArgumentNullException.ThrowIfNull(bannerRepository);

        _bannerRepository = bannerRepository;
    }

    public async Task<OperationResult<DesactivarBannerResponse>> ExecuteAsync(
        DesactivarBannerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.BannerId <= 0)
        {
            return OperationResult<DesactivarBannerResponse>.Failure(
                "El identificador del banner no es válido.");
        }

        var banner = await _bannerRepository.GetByIdAsync(
            request.BannerId,
            cancellationToken);

        if (banner is null)
        {
            return OperationResult<DesactivarBannerResponse>.Failure(
                "El banner indicado no existe.");
        }

        banner.Desactivar();

        _bannerRepository.Update(banner);

        await _bannerRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new DesactivarBannerResponse
        {
            BannerId = banner.Id,
            PublicId = banner.PublicId,
            Titulo = banner.Titulo,
            Activo = banner.Activo
        };

        return OperationResult<DesactivarBannerResponse>.SuccessResult(
            response);
    }
}