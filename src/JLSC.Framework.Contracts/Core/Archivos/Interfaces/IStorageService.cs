using JLSC.Framework.Contracts.Core.Archivos.Models;

namespace JLSC.Framework.Contracts.Core.Archivos.Interfaces;

public interface IStorageService
{
    Task<StorageFileResult> GuardarAsync(
        StorageFileRequest request,
        CancellationToken cancellationToken = default);

    Task<Stream> ObtenerAsync(
        string claveAlmacenamiento,
        CancellationToken cancellationToken = default);

    Task<bool> ExisteAsync(
        string claveAlmacenamiento,
        CancellationToken cancellationToken = default);

    Task EliminarAsync(
        string claveAlmacenamiento,
        CancellationToken cancellationToken = default);
}