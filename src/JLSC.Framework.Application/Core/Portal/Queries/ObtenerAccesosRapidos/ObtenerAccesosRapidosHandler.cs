using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerAccesosRapidos;

public sealed class ObtenerAccesosRapidosHandler
    : IUseCase<
        ObtenerAccesosRapidosRequest,
        List<ObtenerAccesosRapidosResponse>>
{
    private readonly IAccesoRapidoRepository _repository;

    public ObtenerAccesosRapidosHandler(
        IAccesoRapidoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<List<ObtenerAccesosRapidosResponse>>> ExecuteAsync(
        ObtenerAccesosRapidosRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var accesos = await _repository.GetAllAsync(
            request.IncluirInactivos,
            cancellationToken);

        var response = accesos
            .Select(x => new ObtenerAccesosRapidosResponse
            {
                AccesoRapidoId = x.Id,
                PublicId = x.PublicId,
                Titulo = x.Titulo,
                Descripcion = x.Descripcion,
                Icono = x.Icono,
                Enlace = x.Enlace,
                Orden = x.Orden,
                Activo = x.Activo
            })
            .ToList();

        return OperationResult<List<ObtenerAccesosRapidosResponse>>
            .SuccessResult(response);
    }
}