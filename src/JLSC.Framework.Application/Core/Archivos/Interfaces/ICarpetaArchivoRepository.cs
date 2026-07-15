using JLSC.Framework.Domain.Core.Archivos.Entities;

namespace JLSC.Framework.Application.Core.Archivos.Interfaces;

public interface ICarpetaArchivoRepository
{
    Task<CarpetaArchivo?> ObtenerPorCodigoAsync(
        string codigo,
        CancellationToken cancellationToken = default);
}