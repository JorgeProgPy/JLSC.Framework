using JLSC.Framework.Domain.Core.Seguridad.Entities;

namespace JLSC.Framework.Application.Core.Seguridad.Interfaces;

public interface IPermisoRepository
{
    Task<Permiso?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<List<Permiso>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<bool> ExistsCodigoAsync(
        string codigo,
        long? excludeId = null,
        CancellationToken cancellationToken = default);

    void Add(Permiso permiso);

    void Update(Permiso permiso);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}