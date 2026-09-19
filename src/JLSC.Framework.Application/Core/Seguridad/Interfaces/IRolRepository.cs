using JLSC.Framework.Domain.Core.Seguridad.Entities;

namespace JLSC.Framework.Application.Core.Seguridad.Interfaces;

public interface IRolRepository
{
    Task<Rol?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<List<Rol>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<bool> ExistsCodigoAsync(
        string codigo,
        long? excludeId = null,
        CancellationToken cancellationToken = default);

    void Add(Rol rol);

    void Update(Rol rol);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}