using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerProyectosDestacados;

public sealed class ObtenerProyectosDestacadosHandler
    : IUseCase<
        ObtenerProyectosDestacadosRequest,
        List<ObtenerProyectosDestacadosResponse>>
{
    private readonly IProyectoDestacadoRepository _repository;

    public ObtenerProyectosDestacadosHandler(
        IProyectoDestacadoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<
        OperationResult<List<ObtenerProyectosDestacadosResponse>>> ExecuteAsync(
        ObtenerProyectosDestacadosRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var proyectos = await _repository.GetAllAsync(
            request.IncluirInactivos,
            cancellationToken);

        var response = proyectos
            .Select(x => new ObtenerProyectosDestacadosResponse
            {
                ProyectoDestacadoId = x.Id,
                PublicId = x.PublicId,
                Titulo = x.Titulo,
                Descripcion = x.Descripcion,
                Ubicacion = x.Ubicacion,
                ArchivoId = x.ArchivoId,
                Enlace = x.Enlace,
                Orden = x.Orden,
                Activo = x.Activo
            })
            .ToList();

        return OperationResult<
            List<ObtenerProyectosDestacadosResponse>>.SuccessResult(response);
    }
}