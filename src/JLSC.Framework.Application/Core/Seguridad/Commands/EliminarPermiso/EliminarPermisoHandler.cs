using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.EliminarPermiso;

public sealed class EliminarPermisoHandler
    : IUseCase<EliminarPermisoRequest, EliminarPermisoResponse>
{
    private readonly IPermisoRepository _permisoRepository;

    public EliminarPermisoHandler(
        IPermisoRepository permisoRepository)
    {
        ArgumentNullException.ThrowIfNull(permisoRepository);
        _permisoRepository = permisoRepository;
    }

    public async Task<OperationResult<EliminarPermisoResponse>> ExecuteAsync(
        EliminarPermisoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var permiso = await _permisoRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (permiso is null)
        {
            return OperationResult<EliminarPermisoResponse>.Failure(
                "El permiso indicado no existe.");
        }

        permiso.Desactivar();

        _permisoRepository.Update(permiso);

        await _permisoRepository.GuardarCambiosAsync(
            cancellationToken);

        return OperationResult<EliminarPermisoResponse>.SuccessResult(
            new EliminarPermisoResponse
            {
                PermisoId = permiso.Id,
                Codigo = permiso.Codigo,
                Nombre = permiso.Nombre,
                Activo = permiso.Activo
            });
    }
}