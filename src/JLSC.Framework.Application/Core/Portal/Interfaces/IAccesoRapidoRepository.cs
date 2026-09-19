using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Interfaces;

public interface IAccesoRapidoRepository
{
    Task<List<AccesoRapido>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default);

    Task<AccesoRapido?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<AccesoRapido?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default);

    void Add(AccesoRapido acceso);

    void Update(AccesoRapido acceso);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}