using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Comands.ActualizarRol;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarRol;

public sealed class ActualizarRolHandler
    : IUseCase<ActualizarRolRequest, ActualizarRolResponse>
{
    private readonly IRolRepository _rolRepository;

    public ActualizarRolHandler(
        IRolRepository rolRepository)
    {
        ArgumentNullException.ThrowIfNull(rolRepository);
        _rolRepository = rolRepository;
    }

    public async Task<OperationResult<ActualizarRolResponse>> ExecuteAsync(
        ActualizarRolRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rol = await _rolRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (rol is null)
        {
            return OperationResult<ActualizarRolResponse>.Failure(
                "El rol indicado no existe.");
        }

        try
        {
            rol.CambiarNombre(request.Nombre);
            rol.CambiarDescripcion(request.Descripcion);

            if (request.Activo)
            {
                rol.Activar();
            }
            else
            {
                rol.Desactivar();
            }
        }
        catch (ArgumentException ex)
        {
            return OperationResult<ActualizarRolResponse>.Failure(
                ex.Message);
        }

        _rolRepository.Update(rol);

        await _rolRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new ActualizarRolResponse
        {
            RolId = rol.Id,
            Codigo = rol.Codigo,
            Nombre = rol.Nombre,
            Descripcion = rol.Descripcion,
            Activo = rol.Activo
        };

        return OperationResult<ActualizarRolResponse>.SuccessResult(
            response);
    }
}