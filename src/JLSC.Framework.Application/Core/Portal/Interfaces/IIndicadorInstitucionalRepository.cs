using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Interfaces;

public interface IIndicadorInstitucionalRepository
{
    Task<List<IndicadorInstitucional>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default);

    Task<IndicadorInstitucional?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<IndicadorInstitucional?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default);

    void Add(IndicadorInstitucional indicador);

    void Update(IndicadorInstitucional indicador);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}