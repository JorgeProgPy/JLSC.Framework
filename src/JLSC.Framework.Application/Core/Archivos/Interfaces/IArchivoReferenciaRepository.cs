using JLSC.Framework.Domain.Core.Archivos.Entities;

namespace JLSC.Framework.Application.Core.Archivos.Interfaces;

public interface IArchivoReferenciaRepository
{
    Task AgregarAsync(
        ArchivoReferencia archivoReferencia,
        CancellationToken cancellationToken = default);

    Task ActualizarAsync(
        ArchivoReferencia archivoReferencia,
        CancellationToken cancellationToken = default);

    Task<ArchivoReferencia?> ObtenerPorIdAsync(
        long archivoReferenciaId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArchivoReferencia>> ObtenerPorEntidadAsync(
        long moduloId,
        long entidadId,
        CancellationToken cancellationToken = default);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);

    Task<ArchivoReferencia?> ObtenerPrincipalAsync(
    long moduloId,
    long entidadId,
    long tipoUsoId,
    CancellationToken cancellationToken = default);

    Task<bool> ExisteAsync(
        long archivoId,
        long moduloId,
        long entidadId,
        long tipoUsoId,
        CancellationToken cancellationToken = default);
}