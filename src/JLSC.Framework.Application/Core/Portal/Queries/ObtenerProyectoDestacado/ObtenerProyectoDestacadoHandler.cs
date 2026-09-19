using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerProyectoDestacado;

public sealed class ObtenerProyectoDestacadoHandler
    : IUseCase<
        ObtenerProyectoDestacadoRequest,
        ObtenerProyectoDestacadoResponse>
{
    private readonly IProyectoDestacadoRepository _repository;

    public ObtenerProyectoDestacadoHandler(
        IProyectoDestacadoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<ObtenerProyectoDestacadoResponse>> ExecuteAsync(
        ObtenerProyectoDestacadoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var proyecto = await _repository.GetByIdAsync(
            request.ProyectoDestacadoId,
            cancellationToken);

        if (proyecto is null)
        {
            return OperationResult<ObtenerProyectoDestacadoResponse>.Failure(
                "El proyecto destacado no existe.");
        }

        return OperationResult<ObtenerProyectoDestacadoResponse>.SuccessResult(
            new ObtenerProyectoDestacadoResponse
            {
                ProyectoDestacadoId = proyecto.Id,
                PublicId = proyecto.PublicId,
                Titulo = proyecto.Titulo,
                Descripcion = proyecto.Descripcion,
                Ubicacion = proyecto.Ubicacion,
                ArchivoId = proyecto.ArchivoId,
                Enlace = proyecto.Enlace,
                Orden = proyecto.Orden,
                Activo = proyecto.Activo
            });
    }
}