using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Interfaces;

public interface IProyectoDestacadoRepository
{
    Task<List<ProyectoDestacado>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default);

    Task<ProyectoDestacado?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<ProyectoDestacado?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default);

    void Add(ProyectoDestacado proyecto);

    void Update(ProyectoDestacado proyecto);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}