using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Interfaces;

public interface IRedSocialInstitucionalRepository
{
    Task<List<RedSocialInstitucional>> GetAllAsync(long configuracionInstitucionalId, bool incluirInactivos = false, CancellationToken cancellationToken = default);

    Task<RedSocialInstitucional?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<RedSocialInstitucional?> GetByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(long configuracionInstitucionalId, long catalogoItemId, long? excluirId = null, CancellationToken cancellationToken = default);

    void Add(RedSocialInstitucional redSocial);

    void Update(RedSocialInstitucional redSocial);

    Task GuardarCambiosAsync(CancellationToken cancellationToken = default);
}
