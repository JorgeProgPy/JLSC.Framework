using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Domain.Core.Archivos.Constants;
using JLSC.Framework.Application.Core.Archivos.Interfaces;

namespace JLSC.Framework.Application.Core.Archivos.Queries.ObtenerArchivo;

public sealed class ObtenerArchivoHandler
    : IUseCase<ObtenerArchivoRequest, ObtenerArchivoResponse>
{
    private readonly IArchivoRepository _archivoRepository;

    public ObtenerArchivoHandler(
        IArchivoRepository archivoRepository)
    {
        ArgumentNullException.ThrowIfNull(archivoRepository);

        _archivoRepository = archivoRepository;
    }

    public async Task<OperationResult<ObtenerArchivoResponse>> ExecuteAsync(
        ObtenerArchivoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // -----------------------------------------------------------------
        // Validaciones
        // -----------------------------------------------------------------

        if (request.ArchivoId <= 0)
        {
            return OperationResult<ObtenerArchivoResponse>.Failure(
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
            return OperationResult<ObtenerArchivoResponse>.Failure(
                ArchivoMessages.ArchivoNoExiste);
        }

        // -----------------------------------------------------------------
        // Construcción de la respuesta
        // -----------------------------------------------------------------

        var response = new ObtenerArchivoResponse
        {
            ArchivoId = archivo.Id,
            Codigo = archivo.Codigo,
            Nombre = archivo.Nombre,
            NombreOriginal = archivo.NombreOriginal,
            Extension = archivo.Extension,
            MimeType = archivo.MimeType,
            TamanoBytes = archivo.TamanoBytes,
            Descripcion = archivo.Descripcion,
            EsPublico = archivo.EsPublico,
            Carpeta = archivo.CarpetaArchivo.Nombre,
            TipoArchivo = archivo.TipoArchivo.Nombre,
            EstadoArchivo = archivo.EstadoArchivo.Nombre,
            ProveedorAlmacenamiento = archivo.ProveedorAlmacenamiento.Nombre
        };

        // -----------------------------------------------------------------
        // Resultado
        // -----------------------------------------------------------------

        return OperationResult<ObtenerArchivoResponse>.SuccessResult(
            response);
    }
}