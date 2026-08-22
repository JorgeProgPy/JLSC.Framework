using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Domain.Core.Archivos.Constants;
using JLSC.Framework.Application.Core.Archivos.Interfaces;

namespace JLSC.Framework.Application.Core.Archivos.Commands.ActualizarArchivo;

public sealed class ActualizarArchivoHandler
    : IUseCase<ActualizarArchivoRequest, ActualizarArchivoResponse>
{
    private readonly IArchivoRepository _archivoRepository;
    private readonly ICarpetaArchivoRepository _carpetaArchivoRepository;

    public ActualizarArchivoHandler(
        IArchivoRepository archivoRepository,
        ICarpetaArchivoRepository carpetaArchivoRepository)
    {
        ArgumentNullException.ThrowIfNull(archivoRepository);
        ArgumentNullException.ThrowIfNull(carpetaArchivoRepository);

        _archivoRepository = archivoRepository;
        _carpetaArchivoRepository = carpetaArchivoRepository;
    }

    public async Task<OperationResult<ActualizarArchivoResponse>> ExecuteAsync(
        ActualizarArchivoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // -----------------------------------------------------------------
        // Validaciones
        // -----------------------------------------------------------------

        if (request.ArchivoId <= 0)
        {
            return OperationResult<ActualizarArchivoResponse>.Failure(
                ArchivoMessages.ArchivoInvalido);
        }

        if (string.IsNullOrWhiteSpace(request.CodigoCarpeta))
        {
            return OperationResult<ActualizarArchivoResponse>.Failure(
                ArchivoMessages.CarpetaObligatoria);
        }

        // -----------------------------------------------------------------
        // Obtener archivo
        // -----------------------------------------------------------------

        var archivo = await _archivoRepository.ObtenerPorIdAsync(
            request.ArchivoId,
            cancellationToken);

        if (archivo is null)
        {
            return OperationResult<ActualizarArchivoResponse>.Failure(
                ArchivoMessages.ArchivoNoExiste);
        }

        // -----------------------------------------------------------------
        // Obtener carpeta
        // -----------------------------------------------------------------

        var carpeta = await _carpetaArchivoRepository.ObtenerPorCodigoAsync(
            request.CodigoCarpeta,
            cancellationToken);

        if (carpeta is null)
        {
            return OperationResult<ActualizarArchivoResponse>.Failure(
                ArchivoMessages.CarpetaNoExiste(request.CodigoCarpeta));
        }

        if (!carpeta.Activo)
        {
            return OperationResult<ActualizarArchivoResponse>.Failure(
                ArchivoMessages.CarpetaInactiva(request.CodigoCarpeta));
        }

        // -----------------------------------------------------------------
        // Actualizar entidad
        // -----------------------------------------------------------------

        archivo.CambiarDescripcion(request.Descripcion);

        archivo.CambiarVisibilidad(request.EsPublico);

        archivo.MoverACarpeta(carpeta.Id);

        // -----------------------------------------------------------------
        // Persistencia
        // -----------------------------------------------------------------

        await _archivoRepository.ActualizarAsync(
            archivo,
            cancellationToken);

        await _archivoRepository.GuardarCambiosAsync(
            cancellationToken);

        // -----------------------------------------------------------------
        // Construcción de la respuesta
        // -----------------------------------------------------------------

        var response = new ActualizarArchivoResponse
        {
            ArchivoId = archivo.Id,
            Mensaje = ArchivoMessages.ArchivoActualizado
        };

        // -----------------------------------------------------------------
        // Resultado
        // -----------------------------------------------------------------

        return OperationResult<ActualizarArchivoResponse>.SuccessResult(
            response);
    }
}