using JLSC.Framework.Domain.Core.Archivos.Entities;

namespace JLSC.Framework.Application.Core.Archivos.Interfaces;

public interface IArchivoRepository
{
    Task AgregarAsync(
        Archivo archivo,
        CancellationToken cancellationToken = default);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}