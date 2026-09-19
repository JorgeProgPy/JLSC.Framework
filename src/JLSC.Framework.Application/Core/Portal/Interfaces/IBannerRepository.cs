namespace JLSC.Framework.Application.Core.Portal.Interfaces;

using JLSC.Framework.Domain.Core.Portal.Entities;

public interface IBannerRepository
{
    Task<List<Banner>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default);

    Task<Banner?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<Banner?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default);

    void Add(Banner banner);

    void Update(Banner banner);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}