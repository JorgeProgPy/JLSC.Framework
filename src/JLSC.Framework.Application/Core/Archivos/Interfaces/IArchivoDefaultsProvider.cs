namespace JLSC.Framework.Application.Core.Archivos.Interfaces;

public interface IArchivoDefaultsProvider
{
    Task<long> ObtenerTipoArchivoIdAsync(
        CancellationToken cancellationToken = default);

    Task<long> ObtenerEstadoArchivoIdAsync(
        CancellationToken cancellationToken = default);

    Task<long> ObtenerProveedorAlmacenamientoIdAsync(
        CancellationToken cancellationToken = default);
}