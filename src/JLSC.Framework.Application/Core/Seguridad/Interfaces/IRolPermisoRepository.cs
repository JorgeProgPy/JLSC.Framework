using JLSC.Framework.Domain.Core.Seguridad.Entities;

namespace JLSC.Framework.Application.Core.Seguridad.Interfaces;

public interface IRolPermisoRepository
{
    Task<List<RolPermiso>> GetByRolIdAsync(
        long rolId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        long rolId,
        long permisoId,
        CancellationToken cancellationToken = default);

    Task<RolPermiso?> GetAsync(
        long rolId,
        long permisoId,
        CancellationToken cancellationToken = default);

    void Add(RolPermiso rolPermiso);

    void Update(RolPermiso rolPermiso);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}