using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Archivos.Interfaces;

namespace JLSC.Framework.Application.Core.Archivos.Queries.ObtenerArchivosPorEntidad;

public sealed class ObtenerArchivosPorEntidadHandler
    : IUseCase<ObtenerArchivosPorEntidadRequest, IReadOnlyList<ObtenerArchivosPorEntidadResponse>>
{
    private readonly IArchivoReferenciaRepository _archivoReferenciaRepository;

    public ObtenerArchivosPorEntidadHandler(
        IArchivoReferenciaRepository archivoReferenciaRepository)
    {
        ArgumentNullException.ThrowIfNull(archivoReferenciaRepository);

        _archivoReferenciaRepository = archivoReferenciaRepository;
    }

    public async Task<OperationResult<IReadOnlyList<ObtenerArchivosPorEntidadResponse>>> ExecuteAsync(
        ObtenerArchivosPorEntidadRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.ModuloId <= 0)
        {
            return OperationResult<IReadOnlyList<ObtenerArchivosPorEntidadResponse>>.Failure(
                "Debe especificar un módulo válido.");
        }

        if (request.EntidadId <= 0)
        {
            return OperationResult<IReadOnlyList<ObtenerArchivosPorEntidadResponse>>.Failure(
                "Debe especificar una entidad válida.");
        }

        var referencias = await _archivoReferenciaRepository.ObtenerPorEntidadAsync(
            request.ModuloId,
            request.EntidadId,
            cancellationToken);

        var response = referencias
            .Select(x => new ObtenerArchivosPorEntidadResponse
            {
                ArchivoReferenciaId = x.Id,
                ArchivoId = x.ArchivoId,
                ModuloId = x.ModuloId,
                EntidadId = x.EntidadId,
                TipoUsoId = x.TipoUsoId,
                Nombre = x.Archivo.Nombre,
                NombreOriginal = x.Archivo.NombreOriginal,
                Extension = x.Archivo.Extension,
                MimeType = x.Archivo.MimeType,
                TamanoBytes = x.Archivo.TamanoBytes,
                Hash = x.Archivo.Hash,
                EsPublico = x.Archivo.EsPublico,
                Orden = x.Orden,
                EsPrincipal = x.EsPrincipal,
                Activo = x.Activo,
                Observacion = x.Observacion
            })
            .ToList()
            .AsReadOnly();

        return OperationResult<IReadOnlyList<ObtenerArchivosPorEntidadResponse>>
            .SuccessResult(response);
    }
}