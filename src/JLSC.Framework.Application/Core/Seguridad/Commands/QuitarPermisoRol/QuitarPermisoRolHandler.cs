using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.QuitarPermisoRol;

public sealed class QuitarPermisoRolHandler
    : IUseCase<QuitarPermisoRolRequest, QuitarPermisoRolResponse>
{
    private readonly IRolPermisoRepository _rolPermisoRepository;

    public QuitarPermisoRolHandler(
        IRolPermisoRepository rolPermisoRepository)
    {
        ArgumentNullException.ThrowIfNull(rolPermisoRepository);
        _rolPermisoRepository = rolPermisoRepository;
    }

    public async Task<OperationResult<QuitarPermisoRolResponse>> ExecuteAsync(
        QuitarPermisoRolRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var relacion = await _rolPermisoRepository.GetAsync(
            request.RolId,
            request.PermisoId,
            cancellationToken);

        if (relacion is null)
        {
            return OperationResult<QuitarPermisoRolResponse>.Failure(
                "El permiso no está asignado al rol.");
        }

        if (!relacion.Activo)
        {
            return OperationResult<QuitarPermisoRolResponse>.Failure(
                "El permiso ya está inactivo para este rol.");
        }

        relacion.Desactivar();

        _rolPermisoRepository.Update(relacion);

        await _rolPermisoRepository.GuardarCambiosAsync(
            cancellationToken);

        return OperationResult<QuitarPermisoRolResponse>.SuccessResult(
            new QuitarPermisoRolResponse
            {
                RolId = relacion.RolId,
                PermisoId = relacion.PermisoId,
                Activo = relacion.Activo
            });
    }
}