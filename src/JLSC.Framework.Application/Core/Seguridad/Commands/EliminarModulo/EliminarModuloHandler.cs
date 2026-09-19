using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.EliminarModulo;

public sealed class EliminarModuloHandler
    : IUseCase<EliminarModuloRequest, EliminarModuloResponse>
{
    private readonly IModuloRepository _moduloRepository;

    public EliminarModuloHandler(
        IModuloRepository moduloRepository)
    {
        ArgumentNullException.ThrowIfNull(moduloRepository);

        _moduloRepository = moduloRepository;
    }

    public async Task<OperationResult<EliminarModuloResponse>> ExecuteAsync(
        EliminarModuloRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Id <= 0)
        {
            return OperationResult<EliminarModuloResponse>.Failure(
                "El identificador del módulo no es válido.");
        }

        var modulo = await _moduloRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (modulo is null)
        {
            return OperationResult<EliminarModuloResponse>.Failure(
                "El módulo solicitado no existe.");
        }

        if (!modulo.Activo)
        {
            return OperationResult<EliminarModuloResponse>.Failure(
                "El módulo ya se encuentra inactivo.");
        }

        modulo.Desactivar();

        _moduloRepository.Update(modulo);

        await _moduloRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new EliminarModuloResponse
        {
            Id = modulo.Id,
            Codigo = modulo.Codigo,
            Nombre = modulo.Nombre,
            Activo = modulo.Activo
        };

        return OperationResult<EliminarModuloResponse>
            .SuccessResult(response);
    }
}