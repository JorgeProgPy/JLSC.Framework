using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarPermiso;

public sealed class ActualizarPermisoHandler
    : IUseCase<ActualizarPermisoRequest, ActualizarPermisoResponse>
{
    private readonly IPermisoRepository _permisoRepository;

    public ActualizarPermisoHandler(
        IPermisoRepository permisoRepository)
    {
        ArgumentNullException.ThrowIfNull(permisoRepository);
        _permisoRepository = permisoRepository;
    }

    public async Task<OperationResult<ActualizarPermisoResponse>> ExecuteAsync(
        ActualizarPermisoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var permiso = await _permisoRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (permiso is null)
        {
            return OperationResult<ActualizarPermisoResponse>.Failure(
                "El permiso indicado no existe.");
        }

        try
        {
            permiso.CambiarNombre(request.Nombre);
            permiso.CambiarDescripcion(request.Descripcion);
            permiso.CambiarDescripcion(request.Descripcion);

            if (request.Activo)
            {
                permiso.Activar();
            }
            else
            {
                permiso.Desactivar();
            }
        }
        catch (ArgumentException ex)
        {
            return OperationResult<ActualizarPermisoResponse>.Failure(
                ex.Message);
        }

        _permisoRepository.Update(permiso);

        await _permisoRepository.GuardarCambiosAsync(
            cancellationToken);

        return OperationResult<ActualizarPermisoResponse>.SuccessResult(
            new ActualizarPermisoResponse
            {
                PermisoId = permiso.Id,
                Codigo = permiso.Codigo,
                Nombre = permiso.Nombre,
                Descripcion = permiso.Descripcion,
                Activo = permiso.Activo,
                ModuloId = permiso.ModuloId,
                Orden = permiso.Orden
            });
    }
}