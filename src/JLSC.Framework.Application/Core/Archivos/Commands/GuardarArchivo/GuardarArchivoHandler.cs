using JLSC.Framework.Application.Common.Constants.Catalogos;
using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Domain.Core.Archivos.Constants;
using JLSC.Framework.Application.Core.Archivos.Interfaces;
using JLSC.Framework.Application.Core.Catalogos.Interfaces;
using JLSC.Framework.Contracts.Core.Archivos.Interfaces;
using JLSC.Framework.Contracts.Core.Archivos.Models;
using JLSC.Framework.Domain.Core.Archivos.Entities;
using JLSC.Framework.Domain.Core.Archivos.ValueObjects;

namespace JLSC.Framework.Application.Core.Archivos.Commands.GuardarArchivo;

public sealed class GuardarArchivoHandler
    : IUseCase<GuardarArchivoRequest, GuardarArchivoResponse>
{
    private readonly IStorageService _storageService;
    private readonly IArchivoRepository _archivoRepository;
    private readonly ICarpetaArchivoRepository _carpetaArchivoRepository;
    private readonly ICatalogoService _catalogoService;

    public GuardarArchivoHandler(
        IStorageService storageService,
        IArchivoRepository archivoRepository,
        ICarpetaArchivoRepository carpetaArchivoRepository,
        ICatalogoService catalogoService)
    {
        ArgumentNullException.ThrowIfNull(storageService);
        ArgumentNullException.ThrowIfNull(archivoRepository);
        ArgumentNullException.ThrowIfNull(carpetaArchivoRepository);
        ArgumentNullException.ThrowIfNull(catalogoService);

        _storageService = storageService;
        _archivoRepository = archivoRepository;
        _carpetaArchivoRepository = carpetaArchivoRepository;
        _catalogoService = catalogoService;
    }

    public async Task<OperationResult<GuardarArchivoResponse>> ExecuteAsync(
        GuardarArchivoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // -----------------------------------------------------------------
        // Validaciones
        // -----------------------------------------------------------------

        if (request.Contenido is null)
        {
            return OperationResult<GuardarArchivoResponse>.Failure(
                ArchivoMessages.ContenidoObligatorio);
        }

        if (string.IsNullOrWhiteSpace(request.NombreOriginal))
        {
            return OperationResult<GuardarArchivoResponse>.Failure(
                ArchivoMessages.NombreObligatorio);
        }

        if (string.IsNullOrWhiteSpace(request.CodigoCarpeta))
        {
            return OperationResult<GuardarArchivoResponse>.Failure(
                ArchivoMessages.CarpetaObligatoria);
        }

        // -----------------------------------------------------------------
        // Obtener carpeta
        // -----------------------------------------------------------------

        var carpeta = await _carpetaArchivoRepository.ObtenerPorCodigoAsync(
            request.CodigoCarpeta,
            cancellationToken);

        if (carpeta is null)
        {
            return OperationResult<GuardarArchivoResponse>.Failure(
                ArchivoMessages.CarpetaNoExiste(request.CodigoCarpeta));
        }

        if (!carpeta.Activo)
        {
            return OperationResult<GuardarArchivoResponse>.Failure(
                ArchivoMessages.CarpetaInactiva(request.CodigoCarpeta));
        }

        // -----------------------------------------------------------------
        // Almacenar archivo físico
        // -----------------------------------------------------------------

        if (request.Contenido.CanSeek)
        {
            request.Contenido.Position = 0;
        }

        var storageResult = await _storageService.GuardarAsync(
            new StorageFileRequest
            {
                Contenido = request.Contenido,
                NombreOriginal = request.NombreOriginal,
                Carpeta = carpeta.Codigo
            },
            cancellationToken);

        // -----------------------------------------------------------------
        // Determinar tipo de archivo
        // -----------------------------------------------------------------

        var codigoTipoArchivo = ObtenerCodigoTipoArchivo(storageResult.MimeType);

        // -----------------------------------------------------------------
        // Obtener catálogos
        // -----------------------------------------------------------------

        var tipoArchivo = await _catalogoService.ObtenerItemAsync(
            CatalogosCore.TipoArchivo,
            codigoTipoArchivo,
            cancellationToken);

        if (tipoArchivo is null)
        {
            return OperationResult<GuardarArchivoResponse>.Failure(
                ArchivoMessages.TipoArchivoNoConfigurado);
        }

        var estadoArchivo = await _catalogoService.ObtenerItemAsync(
            CatalogosCore.EstadoArchivo,
            EstadoArchivoCodigos.Activo,
            cancellationToken);

        if (estadoArchivo is null)
        {
            return OperationResult<GuardarArchivoResponse>.Failure(
                ArchivoMessages.EstadoArchivoNoConfigurado);
        }

        var proveedorAlmacenamiento = await _catalogoService.ObtenerItemAsync(
            CatalogosCore.ProveedorAlmacenamiento,
            ProveedorAlmacenamientoCodigos.Local,
            cancellationToken);

        if (proveedorAlmacenamiento is null)
        {
            return OperationResult<GuardarArchivoResponse>.Failure(
                ArchivoMessages.ProveedorNoConfigurado);
        }

        // -----------------------------------------------------------------
        // Crear entidad
        // -----------------------------------------------------------------

        var archivo = Archivo.Create(
            new ArchivoStorageInfo(
                storageResult.NombreOriginal,
                storageResult.NombreAlmacenado,
                storageResult.ClaveAlmacenamiento,
                storageResult.Extension,
                storageResult.MimeType,
                storageResult.TamanoBytes,
                storageResult.Hash),
            carpeta.Id,
            tipoArchivo.Id,
            estadoArchivo.Id,
            proveedorAlmacenamiento.Id,
            request.EsPublico,
            propietarioPersonaId: request.PropietarioPersonaId,
            request.Descripcion);

        // -----------------------------------------------------------------
        // Persistencia
        // -----------------------------------------------------------------

        await _archivoRepository.AgregarAsync(
            archivo,
            cancellationToken);

        await _archivoRepository.GuardarCambiosAsync(
            cancellationToken);

        // -----------------------------------------------------------------
        // Construcción de la respuesta
        // -----------------------------------------------------------------

        var response = new GuardarArchivoResponse
        {
            ArchivoId = archivo.Id,
            Nombre = archivo.Nombre,
            NombreOriginal = archivo.NombreOriginal,
            Extension = archivo.Extension,
            MimeType = archivo.MimeType,
            TamanoBytes = archivo.TamanoBytes,
            Hash = archivo.Hash,
            EsPublico = archivo.EsPublico
        };

        return OperationResult<GuardarArchivoResponse>.SuccessResult(response);
    }

    private static string ObtenerCodigoTipoArchivo(string mimeType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(mimeType);

        if (mimeType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            return TipoArchivoCodigos.Imagen;
        }

        if (mimeType.StartsWith("video/", StringComparison.OrdinalIgnoreCase))
        {
            return TipoArchivoCodigos.Video;
        }

        if (mimeType.StartsWith("audio/", StringComparison.OrdinalIgnoreCase))
        {
            return TipoArchivoCodigos.Audio;
        }

        return TipoArchivoCodigos.Documento;
    }
}