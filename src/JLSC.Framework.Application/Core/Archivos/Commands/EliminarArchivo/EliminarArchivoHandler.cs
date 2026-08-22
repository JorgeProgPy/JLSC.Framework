using JLSC.Framework.Application.Common.Constants.Catalogos;
using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Domain.Core.Archivos.Constants;
using JLSC.Framework.Application.Core.Archivos.Interfaces;
using JLSC.Framework.Application.Core.Catalogos.Interfaces;

namespace JLSC.Framework.Application.Core.Archivos.Commands.EliminarArchivo;

public sealed class EliminarArchivoHandler
    : IUseCase<EliminarArchivoRequest, EliminarArchivoResponse>
{
    private readonly IArchivoRepository _archivoRepository;
    private readonly ICatalogoService _catalogoService;

    public EliminarArchivoHandler(
        IArchivoRepository archivoRepository,
        ICatalogoService catalogoService)
    {
        ArgumentNullException.ThrowIfNull(archivoRepository);
        ArgumentNullException.ThrowIfNull(catalogoService);

        _archivoRepository = archivoRepository;
        _catalogoService = catalogoService;
    }

    public async Task<OperationResult<EliminarArchivoResponse>> ExecuteAsync(
        EliminarArchivoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // -----------------------------------------------------------------
        // Validaciones
        // -----------------------------------------------------------------

        if (request.ArchivoId <= 0)
        {
            return OperationResult<EliminarArchivoResponse>.Failure(
                ArchivoMessages.ArchivoInvalido);
        }

        // -----------------------------------------------------------------
        // Obtener archivo
        // -----------------------------------------------------------------

        var archivo = await _archivoRepository.ObtenerPorIdAsync(
            request.ArchivoId,
            cancellationToken);

        if (archivo is null)
        {
            return OperationResult<EliminarArchivoResponse>.Failure(
                ArchivoMessages.ArchivoNoExiste);
        }

        long estadoEliminadoId;

        try
        {
            estadoEliminadoId = await _catalogoService.ObtenerItemIdAsync(
                CatalogosCore.EstadoArchivo,
                EstadoArchivoCodigos.Eliminado,
                cancellationToken);
        }
        catch (InvalidOperationException)
        {
            return OperationResult<EliminarArchivoResponse>.Failure(
                ArchivoMessages.EstadoArchivoNoConfigurado);
        }

        // -----------------------------------------------------------------
        // Validar estado actual
        // -----------------------------------------------------------------

        if (archivo.EstadoArchivoId == estadoEliminadoId)
        {
            return OperationResult<EliminarArchivoResponse>.Failure(
                ArchivoMessages.ArchivoYaEliminado);
        }

        // -----------------------------------------------------------------
        // Actualizar entidad
        // -----------------------------------------------------------------

        archivo.CambiarEstado(estadoEliminadoId);

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

        var response = new EliminarArchivoResponse
        {
            ArchivoId = archivo.Id,
            Mensaje = ArchivoMessages.ArchivoEliminado
        };

        // -----------------------------------------------------------------
        // Resultado
        // -----------------------------------------------------------------

        return OperationResult<EliminarArchivoResponse>.SuccessResult(
            response);
    }
}