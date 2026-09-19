using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Interfaces;

public interface IConfiguracionInstitucionalRepository
{
    Task<ConfiguracionInstitucional?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<ConfiguracionInstitucional?> GetActivaAsync(
        CancellationToken cancellationToken = default);

    void Add(ConfiguracionInstitucional configuracion);

    void Update(ConfiguracionInstitucional configuracion);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}