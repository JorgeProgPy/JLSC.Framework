using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.EliminarRol;

public sealed class EliminarRolHandler
    : IUseCase<EliminarRolRequest, EliminarRolResponse>
{
    private readonly IRolRepository _rolRepository;

    public EliminarRolHandler(
        IRolRepository rolRepository)
    {
        ArgumentNullException.ThrowIfNull(rolRepository);
        _rolRepository = rolRepository;
    }

    public async Task<OperationResult<EliminarRolResponse>> ExecuteAsync(
        EliminarRolRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rol = await _rolRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (rol is null)
        {
            return OperationResult<EliminarRolResponse>.Failure(
                "El rol indicado no existe.");
        }

        rol.Desactivar();

        _rolRepository.Update(rol);

        await _rolRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new EliminarRolResponse
        {
            RolId = rol.Id,
            Codigo = rol.Codigo,
            Nombre = rol.Nombre,
            Activo = rol.Activo
        };

        return OperationResult<EliminarRolResponse>.SuccessResult(
            response);
    }
}