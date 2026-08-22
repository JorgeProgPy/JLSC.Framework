using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Archivos.Interfaces;
using JLSC.Framework.Application.Core.Catalogos.Interfaces;
using JLSC.Framework.Domain.Core.Archivos.Constants;
using JLSC.Framework.Domain.Core.Archivos.Entities;
using LSC.Framework.Domain.Core.Archivos.Constants;


namespace JLSC.Framework.Application.Core.Archivos.Commands.AsociarArchivo;

public sealed class AsociarArchivoHandler
    : IUseCase<AsociarArchivoRequest, AsociarArchivoResponse>
{
    private readonly IArchivoRepository _archivoRepository;
    private readonly IArchivoReferenciaRepository _archivoReferenciaRepository;
    private readonly ICatalogoService _catalogoService;

    public AsociarArchivoHandler(
        IArchivoRepository archivoRepository,
        IArchivoReferenciaRepository archivoReferenciaRepository,
        ICatalogoService catalogoService)
    {
        ArgumentNullException.ThrowIfNull(archivoRepository);
        ArgumentNullException.ThrowIfNull(archivoReferenciaRepository);
        ArgumentNullException.ThrowIfNull(catalogoService);

        _archivoRepository = archivoRepository;
        _archivoReferenciaRepository = archivoReferenciaRepository;
        _catalogoService = catalogoService;
    }

    public async Task<OperationResult<AsociarArchivoResponse>> ExecuteAsync(
        AsociarArchivoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // -----------------------------------------------------------------
        // Obtener archivo
        // -----------------------------------------------------------------

        var archivo = await _archivoRepository.ObtenerPorIdAsync(
            request.ArchivoId,
            cancellationToken);

        if (archivo is null)
        {
            return OperationResult<AsociarArchivoResponse>.Failure(
                ArchivoMessages.ArchivoNoExiste);
        }

        // -----------------------------------------------------------------
        // Validar tipo de uso
        // -----------------------------------------------------------------

        var existeTipoUso = await _catalogoService.ExisteItemAsync(
            request.TipoUsoId,
            cancellationToken);

        if (!existeTipoUso)
        {
            return OperationResult<AsociarArchivoResponse>.Failure(
                ArchivoReferenciaMessages.TipoUsoNoExiste);
                
        }

        // -----------------------------------------------------------------
        // Verificar asociación existente
        // -----------------------------------------------------------------

        var existe = await _archivoReferenciaRepository.ExisteAsync(
            request.ArchivoId,
            request.ModuloId,
            request.EntidadId,
            request.TipoUsoId,
            cancellationToken);

        if (existe)
        {
            return OperationResult<AsociarArchivoResponse>.Failure(
                ArchivoReferenciaMessages.ArchivoYaAsociado);
        }

        // -----------------------------------------------------------------
        // Actualizar referencia principal
        // -----------------------------------------------------------------

        if (request.EsPrincipal)
        {
            var principal = await _archivoReferenciaRepository.ObtenerPrincipalAsync(
                request.ModuloId,
                request.EntidadId,
                request.TipoUsoId,
                cancellationToken);

            if (principal is not null)
            {
                principal.QuitarComoPrincipal();

                await _archivoReferenciaRepository.ActualizarAsync(
                    principal,
                    cancellationToken);
            }
        }

        // -----------------------------------------------------------------
        // Crear referencia
        // -----------------------------------------------------------------

        var referencia = ArchivoReferencia.Create(
            request.ArchivoId,
            request.ModuloId,
            request.EntidadId,
            request.TipoUsoId,
            request.Orden,
            request.EsPrincipal,
            request.Observacion);

        // -----------------------------------------------------------------
        // Persistencia
        // -----------------------------------------------------------------

        await _archivoReferenciaRepository.AgregarAsync(
            referencia,
            cancellationToken);

        await _archivoReferenciaRepository.GuardarCambiosAsync(
            cancellationToken);

        // -----------------------------------------------------------------
        // Construcción de la respuesta
        // -----------------------------------------------------------------

        var response = new AsociarArchivoResponse
        {
            ArchivoReferenciaId = referencia.Id,
            ArchivoId = referencia.ArchivoId,
            ModuloId = referencia.ModuloId,
            EntidadId = referencia.EntidadId,
            TipoUsoId = referencia.TipoUsoId,
            Orden = referencia.Orden,
            EsPrincipal = referencia.EsPrincipal,
            Activo = referencia.Activo,
            Mensaje = ArchivoReferenciaMessages.ArchivoAsociado
        };

        // -----------------------------------------------------------------
        // Resultado
        // -----------------------------------------------------------------

        return OperationResult<AsociarArchivoResponse>.SuccessResult(
            response);
    }
}