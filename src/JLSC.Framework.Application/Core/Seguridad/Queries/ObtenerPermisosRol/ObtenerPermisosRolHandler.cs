using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerPermisosRol;

public sealed class ObtenerPermisosRolHandler
    : IUseCase<ObtenerPermisosRolRequest, List<ObtenerPermisosRolResponse>>
{
    private readonly IRolRepository _rolRepository;
    private readonly IRolPermisoRepository _rolPermisoRepository;
    private readonly IPermisoRepository _permisoRepository;

    public ObtenerPermisosRolHandler(
        IRolRepository rolRepository,
        IRolPermisoRepository rolPermisoRepository,
        IPermisoRepository permisoRepository)
    {
        ArgumentNullException.ThrowIfNull(rolRepository);
        ArgumentNullException.ThrowIfNull(rolPermisoRepository);
        ArgumentNullException.ThrowIfNull(permisoRepository);

        _rolRepository = rolRepository;
        _rolPermisoRepository = rolPermisoRepository;
        _permisoRepository = permisoRepository;
    }

    public async Task<OperationResult<List<ObtenerPermisosRolResponse>>> ExecuteAsync(
        ObtenerPermisosRolRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rol = await _rolRepository.GetByIdAsync(
            request.RolId,
            cancellationToken);

        if (rol is null)
        {
            return OperationResult<List<ObtenerPermisosRolResponse>>.Failure(
                "El rol indicado no existe.");
        }

        var relaciones = await _rolPermisoRepository.GetByRolIdAsync(
            request.RolId,
            cancellationToken);

        var resultado = new List<ObtenerPermisosRolResponse>();

        foreach (var relacion in relaciones.Where(x => x.Activo))
        {
            var permiso = await _permisoRepository.GetByIdAsync(
                relacion.PermisoId,
                cancellationToken);

            if (permiso is null)
            {
                continue;
            }

            resultado.Add(new ObtenerPermisosRolResponse
            {
                PermisoId = permiso.Id,
                Codigo = permiso.Codigo,
                Nombre = permiso.Nombre,
                ModuloId = permiso.ModuloId,
                Activo = permiso.Activo
            });
        }

        return OperationResult<List<ObtenerPermisosRolResponse>>
            .SuccessResult(resultado);
    }
}