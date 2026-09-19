using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerRoles;

public sealed class ObtenerRolesHandler
    : IUseCase<ObtenerRolesRequest, List<ObtenerRolesResponse>>
{
    private readonly IRolRepository _rolRepository;

    public ObtenerRolesHandler(
        IRolRepository rolRepository)
    {
        ArgumentNullException.ThrowIfNull(rolRepository);
        _rolRepository = rolRepository;
    }

    public async Task<OperationResult<List<ObtenerRolesResponse>>> ExecuteAsync(
        ObtenerRolesRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var roles = await _rolRepository.GetAllAsync(
            cancellationToken);

        var response = roles
            .Where(x => request.IncluirInactivos || x.Activo)
            .Select(x => new ObtenerRolesResponse
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Activo = x.Activo
            })
            .ToList();

        return OperationResult<List<ObtenerRolesResponse>>
            .SuccessResult(response);
    }
}