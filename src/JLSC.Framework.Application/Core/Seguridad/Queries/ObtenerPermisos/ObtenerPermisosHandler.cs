using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerPermisos;

public sealed class ObtenerPermisosHandler
    : IUseCase<ObtenerPermisosRequest, List<ObtenerPermisosResponse>>
{
    private readonly IPermisoRepository _permisoRepository;

    public ObtenerPermisosHandler(
        IPermisoRepository permisoRepository)
    {
        ArgumentNullException.ThrowIfNull(permisoRepository);
        _permisoRepository = permisoRepository;
    }

    public async Task<OperationResult<List<ObtenerPermisosResponse>>> ExecuteAsync(
        ObtenerPermisosRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var permisos = await _permisoRepository.GetAllAsync(
            cancellationToken);

        var response = permisos
            .Where(x => request.IncluirInactivos || x.Activo)
            .Select(x => new ObtenerPermisosResponse
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Activo = x.Activo
            })
            .ToList();

        return OperationResult<List<ObtenerPermisosResponse>>
            .SuccessResult(response);
    }
}