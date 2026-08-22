using JLSC.Framework.Domain.Core.Seguridad.Entities;

namespace JLSC.Framework.Application.Core.Seguridad.Interfaces;

public interface IModuloRepository
{
    // ==========================
    // Consultas
    // ==========================

    Task<Modulo?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<Modulo?> GetByCodigoAsync(
        string codigo,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsCodigoAsync(
        string codigo,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Modulo>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default);

    // ==========================
    // Comandos
    // ==========================

    Task AddAsync(
        Modulo modulo,
        CancellationToken cancellationToken = default);

    void Update(Modulo modulo);

    void Remove(Modulo modulo);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}