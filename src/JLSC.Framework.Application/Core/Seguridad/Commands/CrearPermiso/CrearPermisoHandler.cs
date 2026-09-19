using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Entities;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.CrearPermiso;

public sealed class CrearPermisoHandler
    : IUseCase<CrearPermisoRequest, CrearPermisoResponse>
{
    private readonly IPermisoRepository _permisoRepository;

    public CrearPermisoHandler(
        IPermisoRepository permisoRepository)
    {
        ArgumentNullException.ThrowIfNull(permisoRepository);
        _permisoRepository = permisoRepository;
    }

    public async Task<OperationResult<CrearPermisoResponse>> ExecuteAsync(
        CrearPermisoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Codigo))
        {
            return OperationResult<CrearPermisoResponse>.Failure(
                "El código del permiso es obligatorio.");
        }

        if (await _permisoRepository.ExistsCodigoAsync(
                request.Codigo,
                null,
                cancellationToken))
        {
            return OperationResult<CrearPermisoResponse>.Failure(
                "Ya existe un permiso con el código indicado.");
        }

        Permiso permiso;

        try
        {
            permiso = Permiso.Crear(
                request.ModuloId,
                request.Codigo,
                request.Nombre,
                request.Descripcion,
                request.Orden);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<CrearPermisoResponse>.Failure(
                ex.Message);
        }

        _permisoRepository.Add(permiso);

        await _permisoRepository.GuardarCambiosAsync(
            cancellationToken);

        return OperationResult<CrearPermisoResponse>.SuccessResult(
            new CrearPermisoResponse
            {
                PermisoId = permiso.Id,
                ModuloId = permiso.ModuloId,
                Codigo = permiso.Codigo,
                Nombre = permiso.Nombre,
                Descripcion = permiso.Descripcion,
                Orden = permiso.Orden
            });
    }
}