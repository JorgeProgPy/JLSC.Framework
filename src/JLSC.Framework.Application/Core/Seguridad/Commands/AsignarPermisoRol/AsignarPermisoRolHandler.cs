using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Entities;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.AsignarPermisoRol;

public sealed class AsignarPermisoRolHandler
    : IUseCase<AsignarPermisoRolRequest, AsignarPermisoRolResponse>
{
    private readonly IRolPermisoRepository _rolPermisoRepository;
    private readonly IRolRepository _rolRepository;
    private readonly IPermisoRepository _permisoRepository;

    public AsignarPermisoRolHandler(
        IRolPermisoRepository rolPermisoRepository,
        IRolRepository rolRepository,
        IPermisoRepository permisoRepository)
    {
        ArgumentNullException.ThrowIfNull(rolPermisoRepository);
        ArgumentNullException.ThrowIfNull(rolRepository);
        ArgumentNullException.ThrowIfNull(permisoRepository);

        _rolPermisoRepository = rolPermisoRepository;
        _rolRepository = rolRepository;
        _permisoRepository = permisoRepository;
    }

    public async Task<OperationResult<AsignarPermisoRolResponse>> ExecuteAsync(
        AsignarPermisoRolRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rol = await _rolRepository.GetByIdAsync(
            request.RolId,
            cancellationToken);

        if (rol is null)
        {
            return OperationResult<AsignarPermisoRolResponse>.Failure(
                "El rol indicado no existe.");
        }

        if (!rol.Activo)
        {
            return OperationResult<AsignarPermisoRolResponse>.Failure(
                "No se puede asignar permisos a un rol inactivo.");
        }

        var permiso = await _permisoRepository.GetByIdAsync(
            request.PermisoId,
            cancellationToken);

        if (permiso is null)
        {
            return OperationResult<AsignarPermisoRolResponse>.Failure(
                "El permiso indicado no existe.");
        }

        if (!permiso.Activo)
        {
            return OperationResult<AsignarPermisoRolResponse>.Failure(
                "No se puede asignar un permiso inactivo.");
        }

        var relacion = await _rolPermisoRepository.GetAsync(
            request.RolId,
            request.PermisoId,
            cancellationToken);

        if (relacion is not null)
        {
            if (!relacion.Activo)
            {
                relacion.Activar();

                _rolPermisoRepository.Update(relacion);

                await _rolPermisoRepository.GuardarCambiosAsync(
                    cancellationToken);

                return OperationResult<AsignarPermisoRolResponse>.SuccessResult(
                    new AsignarPermisoRolResponse
                    {
                        RolId = relacion.RolId,
                        PermisoId = relacion.PermisoId,
                        Activo = relacion.Activo
                    });
            }

            return OperationResult<AsignarPermisoRolResponse>.Failure(
                "El permiso ya está asignado al rol.");
        }

        relacion = RolPermiso.Crear(
            request.RolId,
            request.PermisoId);

        _rolPermisoRepository.Add(relacion);

        await _rolPermisoRepository.GuardarCambiosAsync(
            cancellationToken);

        return OperationResult<AsignarPermisoRolResponse>.SuccessResult(
            new AsignarPermisoRolResponse
            {
                RolId = relacion.RolId,
                PermisoId = relacion.PermisoId,
                Activo = relacion.Activo
            });
    }
}