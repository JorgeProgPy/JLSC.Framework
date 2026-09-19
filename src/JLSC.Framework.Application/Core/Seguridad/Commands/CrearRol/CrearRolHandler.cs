using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Entities;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.CrearRol;

public sealed class CrearRolHandler
    : IUseCase<CrearRolRequest, CrearRolResponse>
{
    private readonly IRolRepository _rolRepository;

    public CrearRolHandler(
        IRolRepository rolRepository)
    {
        ArgumentNullException.ThrowIfNull(rolRepository);
        _rolRepository = rolRepository;
    }

    public async Task<OperationResult<CrearRolResponse>> ExecuteAsync(
        CrearRolRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Codigo))
        {
            return OperationResult<CrearRolResponse>.Failure(
                "El código del rol es obligatorio.");
        }

        if (await _rolRepository.ExistsCodigoAsync(
                request.Codigo,
                null,
                cancellationToken))
        {
            return OperationResult<CrearRolResponse>.Failure(
                "Ya existe un rol con el código indicado.");
        }

        Rol rol;

        try
        {
            rol = Rol.Crear(
                request.Codigo,
                request.Nombre,
                request.Descripcion);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<CrearRolResponse>.Failure(
                ex.Message);
        }

        _rolRepository.Add(rol);

        await _rolRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new CrearRolResponse
        {
            RolId = rol.Id,
            Codigo = rol.Codigo,
            Nombre = rol.Nombre
        };

        return OperationResult<CrearRolResponse>
            .SuccessResult(response);
    }
}