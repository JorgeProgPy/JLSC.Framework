using JLSC.Framework.Domain.Core.Catalogos.Entities;

namespace JLSC.Framework.Application.Core.Catalogos.Interfaces;

public interface ICatalogoService
{
    Task<CatalogoItem?> ObtenerItemAsync(
        string codigoCatalogo,
        string codigoItem,
        CancellationToken cancellationToken = default);

    Task<long> ObtenerItemIdAsync(
        string codigoCatalogo,
        string codigoItem,
        CancellationToken cancellationToken = default);
}